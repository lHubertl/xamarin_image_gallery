using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using ImageGallery.Core.Commands;
using ImageGallery.Core.DependencyServices;
using ImageGallery.Core.Infrastructure;
using ImageGallery.Core.Managers;
using ImageGallery.Core.Resources;
using ImageGallery.Services;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace ImageGallery.ViewModels
{
    public class UploadNewPicturePageViewModel : ViewModelBase
    {
        private readonly IImageService _imageService;
        private readonly IGeolocationService _geolocationService;

        private MemoryStream _imageStream;

        private double _longtitude;
        private double _latitude;

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private string _hashtag;
        public string Hashtag
        {
            get => _hashtag;
            set => SetProperty(ref _hashtag, value);
        }

        private ImageSource _userImageSource = ImageSource.FromFile("image.png");
        public ImageSource UserImageSource
        {
            get => _userImageSource;
            set => SetProperty(ref _userImageSource, value);
        }

        public ICommand SelectImageCommand => new SingleExecutionCommand(ExecuteSelectImageCommand);
        public ICommand SaveImageCommand => new SingleExecutionCommand(ExecuteSaveImageCommand);

        public UploadNewPicturePageViewModel(INavigationService navigationService, 
                                             IPageDialogService dialogService,
                                             IImageService imageService,
                                             IGeolocationService geolocationService) : base(navigationService, dialogService)
        {
            _imageService = imageService;
            _geolocationService = geolocationService;
        }

        private async Task ExecuteSelectImageCommand()
        {
            var stream = await Xamarin.Forms.DependencyService.Get<IPicturePicker>().GetImageStreamAsync();

            if (stream != null)
            {
                var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                UserImageSource = ImageSource.FromStream(() => new MemoryStream(memoryStream.ToArray()));
                _imageStream = memoryStream;
            }
        }

        private async Task ExecuteSaveImageCommand()
        {
            await GetUserCurrentLocation();

            var validation = GetValidationManager();

            if (!validation.IsValid)
            {
                var errorMessage = string.Join("\n", validation.Errors);
                await UserNotificationAsync(errorMessage, Strings.Warning);
                return;
            }

            IsBusy = true;

            var result = await PerformDataRequestAsync(() => _imageService.PostImage(_latitude, _longtitude, Description, Hashtag, _imageStream, CancellationToken.None), false);
            if (result)
            {
                await NavigationService.GoBackAsync();
            }

            IsBusy = false;
        }

        private ValidationManager GetValidationManager()
        {
            return ValidationManager.Create().Validate(() => _imageStream != null, Strings.V_Image)
                                    .Validate(() => !(Math.Abs(_latitude) < 0.00001) && !(Math.Abs(_longtitude) < 0.00001), Strings.V_Location); 
                                    // TODO: Compare to Location (Xamarin.Essentials)
        }

        private async Task GetUserCurrentLocation()
        {
            var location = await _geolocationService.GetCurrentLocation();

            _latitude = location.Latitude;
            _longtitude = location.Longitude;
        }
    }
}
