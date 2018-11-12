using ImageGallery.Core.Infrastructure;
using Prism.Navigation;
using Prism.Services;

namespace ImageGallery.ViewModels
{
	public class SignInPageViewModel : ViewModelBase
	{
	    public SignInPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
	    {
	    }
	}
}
