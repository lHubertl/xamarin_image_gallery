using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using ImageGallery.Core.Commands;
using ImageGallery.Core.Infrastructure;
using ImageGallery.Models;
using ImageGallery.Services;
using Prism.Navigation;
using Prism.Services;

namespace ImageGallery.ViewModels
{
	public class ImagesPageViewModel : ViewModelBase
	{
	    private readonly IImageService _imageService;

        private List<ImageModel> _images;
        public List<ImageModel> Images
        {
            get => _images;
            set => SetProperty(ref _images, value);
        }

        public ICommand ShowGifCommand => new SingleExecutionCommand(ExecuteShowGifCommand);
	    public ICommand AddImageCommand => new SingleExecutionCommand(ExecuteAddImageCommand);

	    public ImagesPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IImageService imageService) : base(navigationService, dialogService)
	    {
	        _imageService = imageService;
	    }

	    public override async void OnNavigatedTo(INavigationParameters parameters)
	    {
	        base.OnNavigatingTo(parameters);

	        IsBusy = true;

	        Images = await PerformDataRequestAsync(() => _imageService.GetAllImages(CancellationToken.None));

	        IsBusy = false;
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
