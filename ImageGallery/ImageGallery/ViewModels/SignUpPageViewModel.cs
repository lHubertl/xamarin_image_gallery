using System;
using System.IO;
using System.Threading;
using System.Windows.Input;
using ImageGallery.Core.BusinessLogic.Repositories;
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

        private FileStream _imageStream;
	    public FileStream ImageStream
        {
            get => _imageStream;
            set => SetProperty(ref _imageStream, value);
        }

        public ICommand SignUpCommand => new Command(ExecuteSignUpCommand);

	    public ICommand SelectImageCommand => new Command(ExecuteSelectImageCommand);

	    public SignUpPageViewModel(
	        INavigationService navigationService, 
	        IPageDialogService dialogService,
	        ILoginService loginService,
	        IDataRepository dataRepository) : base(navigationService, dialogService)
        {
            _loginService = loginService;
            _dataRepository = dataRepository;
        }

	    private async void ExecuteSignUpCommand()
	    {
	        if (!IsDataValid())
	        {
                return;
	        }

	        var result = await PerformDataRequestAsync(() => _loginService.SignUpAsync(UserName, Email, Password, ImageStream, CancellationToken.None));
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

	    private void ExecuteSelectImageCommand()
	    {

	    }

	    private bool IsDataValid()
	    {
	        return !string.IsNullOrWhiteSpace(Email) &&
	               !string.IsNullOrWhiteSpace(Password) &&
	               ImageStream != null;
	    }
    }
}
