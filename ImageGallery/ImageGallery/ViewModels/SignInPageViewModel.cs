using ImageGallery.Core.Infrastructure;
using Prism.Navigation;

namespace ImageGallery.ViewModels
{
	public class SignInPageViewModel : ViewModelBase
	{
        public SignInPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}
