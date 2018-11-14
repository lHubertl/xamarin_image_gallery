using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using ImageGallery.Core.Commands;
using ImageGallery.Core.Infrastructure;
using ImageGallery.Core.Managers;
using ImageGallery.Core.Resources;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace ImageGallery.ViewModels
{
    public class UploadNewPicturePageViewModel : ViewModelBase
    {
        private Stream _image;
        public Stream Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

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

        private float _longtitude;
        public float Longtitude
        {
            get => _longtitude;
            set => SetProperty(ref _longtitude, value);
        }

        private float _latitude;
        public float Latitude
        {
            get => _latitude;
            set => SetProperty(ref _latitude, value);
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
                                             IPageDialogService dialogService) : base(navigationService, dialogService)
        {
        }

        private async Task ExecuteSelectImageCommand()
        {
            throw new NotImplementedException();
        }

        private async Task ExecuteSaveImageCommand()
        {
            var validation = GetValidationManager();

            if (!validation.IsValid)
            {
                var errorMessage = string.Join("\n", validation.Errors);
                await UserNotificationAsync(errorMessage, Strings.Warning);
                return;
            }

            IsBusy = true;
        }

        private ValidationManager GetValidationManager()
        {
            return ValidationManager.Create().Validate(() => Image != null, Strings.V_Image);
        }
    }
}
