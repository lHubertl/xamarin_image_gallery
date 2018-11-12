using Prism.Navigation;
using ImageGallery.Core.Infrastructure;

namespace ImageGallery.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {

        }
    }
}
