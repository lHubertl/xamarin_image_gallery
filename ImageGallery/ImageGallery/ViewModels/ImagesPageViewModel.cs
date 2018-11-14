using System.Threading.Tasks;
using System.Windows.Input;
using ImageGallery.Core.Commands;
using ImageGallery.Core.Infrastructure;
using Prism.Navigation;
using Prism.Services;

namespace ImageGallery.ViewModels
{
	public class ImagesPageViewModel : ViewModelBase
	{
        public ICommand ShowGifCommand => new SingleExecutionCommand(ExecuteShowGifCommand);
	    public ICommand AddImageCommand => new SingleExecutionCommand(ExecuteAddImageCommand);

	    public ImagesPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
        }

	    private Task ExecuteShowGifCommand()
	    {
	        throw new System.NotImplementedException();
	    }

	    private Task ExecuteAddImageCommand()
	    {
	        throw new System.NotImplementedException();
	    }
    }
}
