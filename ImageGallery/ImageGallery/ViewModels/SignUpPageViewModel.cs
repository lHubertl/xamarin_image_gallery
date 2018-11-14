using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using ImageGallery.Constants;
using ImageGallery.Core.BusinessLogic.Repositories;
using ImageGallery.Core.Commands;
using ImageGallery.Core.DependencyServices;
using ImageGallery.Core.Infrastructure;
using ImageGallery.Core.Managers;
using ImageGallery.Core.Resources;
using ImageGallery.Services;
using ImageGallery.Views;
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
	    private MemoryStream _imageStream;

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
	        var validation = GetValidationManager();

            if (!validation.IsValid)
            {
                var errorMessage = string.Join("\n", validation.Errors);
                await UserNotificationAsync(errorMessage, Strings.Warning);
                return;
	        }

	        IsBusy = true;

            var result = await PerformDataRequestAsync(() => _loginService.SignUpAsync(UserName, Email, Password, _imageStream, CancellationToken.None));
	        if (result != null)
	        {
	            try
	            {
	                await SecureStorage.SetAsync(DataType.Token.ToString(), result.Token);
	                await SecureStorage.SetAsync(DataType.AvatarUrl.ToString(), result.AvatarUrl);
                }
	            catch (Exception ex)
	            {
	                // Possible that device doesn't support secure storage on device.
	            }

                // Save to the memory
	            _dataRepository.Set(DataType.Token, result.Token);
	            _dataRepository.Set(DataType.AvatarUrl, result.AvatarUrl);

	            // TODO: navigate to image page
	        }

	        IsBusy = false;
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
                _imageStream = memoryStream;
	        }
        }

	    private Task ExecuteSignInCommand()
	    {
	        var navigationParameters = new NavigationParameters
	        {
	            { NavParamKeys.Email, Email },
	            { NavParamKeys.Password, Password }
            };

	        return NavigationService.NavigateAsync(nameof(SignInPage), navigationParameters);
	    }

        private ValidationManager GetValidationManager()
        {
            return ValidationManager.Create().Validate(() => !string.IsNullOrWhiteSpace(Email), Strings.V_Email)
                .Validate(() => !string.IsNullOrWhiteSpace(Password), Strings.V_Password)
                .Validate(() => _imageStream != null, Strings.V_Image);
	    }
    }
}
