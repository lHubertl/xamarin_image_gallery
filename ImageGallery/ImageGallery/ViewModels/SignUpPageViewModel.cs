using System.Windows.Input;
using ImageGallery.Core.Infrastructure;
using ImageGallery.Services;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace ImageGallery.ViewModels
{
	public class SignUpPageViewModel : ViewModelBase
	{
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

        public ICommand SignUpCommand => new Command(ExecuteSignUpCommand);

	    public ICommand SelectImageCommand => new Command(ExecuteSelectImageCommand);

	    public SignUpPageViewModel(
	        INavigationService navigationService, 
	        IPageDialogService dialogService,
	        ILoginService loginService) : base(navigationService, dialogService)
        {
            _loginService = loginService;
        }

	    private void ExecuteSignUpCommand()
	    {

	    }

	    private void ExecuteSelectImageCommand()
	    {

	    }
    }
}
