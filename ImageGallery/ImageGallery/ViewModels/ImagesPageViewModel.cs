using System.Threading.Tasks;
using System.Windows.Input;
using ImageGallery.Core.Commands;
using ImageGallery.Core.Infrastructure;
using ImageGallery.Views;
using ImageGallery.Services;
using Prism.Navigation;
using Prism.Services;

namespace ImageGallery.ViewModels
{
	public class ImagesPageViewModel : ViewModelBase
	{
	    private readonly IImageService _imageService;

	    public ICommand ShowGifCommand => new SingleExecutionCommand(ExecuteShowGifCommand);
	    public ICommand AddImageCommand => new SingleExecutionCommand(ExecuteAddImageCommand);

	    public ImagesPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IImageService imageService) : base(navigationService, dialogService)
	    {
	        _imageService = imageService;
	    }

	    private Task ExecuteShowGifCommand()
	    {
	        throw new System.NotImplementedException();
	    }

	    private Task ExecuteAddImageCommand()
	    {
            return NavigationService.NavigateAsync(nameof(UploadNewPicturePage));
	    }
    }
}
