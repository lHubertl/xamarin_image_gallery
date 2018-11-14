using System.Threading.Tasks;
using System.Windows.Input;
using ImageGallery.Core.Commands;
using ImageGallery.Core.Infrastructure;
using ImageGallery.Views;
using Prism.Navigation;
using Prism.Services;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace ImageGallery.ViewModels
{
	public class ImagesPageViewModel : ViewModelBase
	{
        public ICommand ShowGifCommand => new Command(ExecuteShowGifCommand);
	    public ICommand AddImageCommand => new SingleExecutionCommand(ExecuteAddImageCommand);

	    public ImagesPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
        }

	    private async void ExecuteShowGifCommand()
	    {
            await NavigationService.NavigateAsync(nameof(GifPopupPage));
           //await PopupNavigation.Instance.PushAsync(new GifPopupPage());
	    }

	    private Task ExecuteAddImageCommand()
	    {
	        throw new System.NotImplementedException();
	    }
    }
}
