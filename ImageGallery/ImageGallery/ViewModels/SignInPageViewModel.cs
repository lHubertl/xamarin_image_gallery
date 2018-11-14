using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using ImageGallery.Constants;
using ImageGallery.Core.BusinessLogic.Repositories;
using ImageGallery.Core.Commands;
using ImageGallery.Core.Infrastructure;
using ImageGallery.Core.Managers;
using ImageGallery.Core.Resources;
using ImageGallery.Services;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Essentials;

namespace ImageGallery.ViewModels
{
	public class SignInPageViewModel : ViewModelBase
	{
	    private readonly ILoginService _loginService;
	    private readonly IDataRepository _dataRepository;

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

	    public ICommand SignInCommand => new SingleExecutionCommand(ExecuteSignInCommand);

	    public SignInPageViewModel(INavigationService navigationService, IPageDialogService dialogService, ILoginService loginService, IDataRepository dataRepository) : base(navigationService, dialogService)
	    {
	        _loginService = loginService;
	        _dataRepository = dataRepository;
	    }

	    public override void OnNavigatingTo(INavigationParameters parameters)
	    {
	        base.OnNavigatingTo(parameters);

	        if (parameters != null)
	        {
	            if (parameters.TryGetValue(NavParamKeys.Email, out string email))
	            {
	                Email = email;
	            }

	            if (parameters.TryGetValue(NavParamKeys.Password, out string password))
	            {
	                Password = password;
	            }
            }
	    }

	    private async Task ExecuteSignInCommand()
	    {
	        var validation = GetValidationManager();

	        if (!validation.IsValid)
	        {
	            var errorMessage = string.Join("\n", validation.Errors);
	            await UserNotificationAsync(errorMessage, Strings.Warning);
	            return;
	        }

	        IsBusy = true;

            var result = await PerformDataRequestAsync(() => _loginService.SignInAsync(Email, Password, CancellationToken.None));
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

        private ValidationManager GetValidationManager()
	    {
	        return ValidationManager.Create().Validate(() => !string.IsNullOrWhiteSpace(Email), Strings.V_Email)
	            .Validate(() => !string.IsNullOrWhiteSpace(Password), Strings.V_Password);
	    }
    }
}
