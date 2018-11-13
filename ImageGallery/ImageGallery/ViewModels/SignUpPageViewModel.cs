using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using ImageGallery.Core.BusinessLogic.Repositories;
using ImageGallery.Core.Commands;
using ImageGallery.Core.DependencyServices;
using ImageGallery.Core.Infrastructure;
using ImageGallery.Models;
using ImageGallery.Services;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ImageGallery.ViewModels
{
	public class SignUpPageViewModel : ViewModelBase
	{
	    private readonly IDataRepository _dataRepository;
        private readonly ILoginService _loginService;
	    private Stream _imageStream;

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private ImageSource _userImageSource;
        public ImageSource UserImageSource
        {
            get => _userImageSource;
            set => SetProperty(ref _userImageSource, value);
        }

        public ICommand SignInCommand => new SingleExecutionCommand(ExecuteSignInCommand);

	    public ICommand SignUpCommand => new SingleExecutionCommand(ExecuteSignUpCommand);

	    public ICommand SelectImageCommand => new SingleExecutionCommand(ExecuteSelectImageCommand);

	    public SignUpPageViewModel(
	        INavigationService navigationService, 
	        IPageDialogService dialogService,
	        ILoginService loginService,
	        IDataRepository dataRepository) : base(navigationService, dialogService)
        {
            _loginService = loginService;
            _dataRepository = dataRepository;
        }

	    private async Task ExecuteSignUpCommand()
	    {
	        if (!IsDataValid())
	        {
                return;
	        }

	        var result = await PerformDataRequestAsync(() => _loginService.SignUpAsync(UserName, Email, Password, _imageStream, CancellationToken.None));
	        if (result != null)
	        {
	            try
	            {
	                await SecureStorage.SetAsync(nameof(LoginModel.Token), result.Token);
	            }
	            catch (Exception ex)
	            {
	                // Possible that device doesn't support secure storage on device.
	            }

                // Save to the memory
	            _dataRepository.Set(DataType.Token, result.Token);

                // TODO: navigate to image page
            }
	    }

	    private async Task ExecuteSelectImageCommand()
	    {
	        var stream = await Xamarin.Forms.DependencyService.Get<IPicturePicker>().GetImageStreamAsync();

	        if (stream != null)
	        {
                var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                UserImageSource = ImageSource.FromStream(() => new MemoryStream(memoryStream.ToArray()));
                _imageStream = stream;

	        }
        }

	    private async Task ExecuteSignInCommand()
	    {
	        // TODO: navigate to sign in page
	    }

        private bool IsDataValid()
	    {
	        return !string.IsNullOrWhiteSpace(Email) &&
	               !string.IsNullOrWhiteSpace(Password) &&
	               _imageStream != null;
	    }
    }
}
