using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using ImageGallery.Core.Commands;
using ImageGallery.Core.Infrastructure;
using ImageGallery.Views;
using ImageGallery.Models;
using ImageGallery.Services;
using Prism.Navigation;
using Prism.Services;
using ImageGallery.Constants;

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
        public ICommand TapOnImageCommand => new SingleExecutionCommand(ExecuteTapOnImageCommand);

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

	    private async Task ExecuteShowGifCommand()
	    {
            var gifUrl = await PerformDataRequestAsync (() => _imageService.GetGif(CancellationToken.None));
            if(!string.IsNullOrEmpty(gifUrl))
            {
                var navigationParams = new NavigationParameters
                {
                    {NavParamKeys.GifUrl, gifUrl}
                };

                await NavigationService.NavigateAsync(nameof(GifPopupPage), navigationParams);
            }

	    }

	    private Task ExecuteAddImageCommand()
	    {
	        return NavigationService.NavigateAsync(nameof(UploadNewPicturePage));
	    }

	    private async Task ExecuteTapOnImageCommand(object arg)
	    {
	        if (arg is ImageModel imageModel)
	        {
                // TODO: open popup with large image
	        }
	    }
    }
}
