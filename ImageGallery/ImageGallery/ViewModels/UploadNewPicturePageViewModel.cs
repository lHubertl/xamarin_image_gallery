using System;
using System.IO;
using ImageGallery.Core.Infrastructure;
using Prism.Navigation;
using Prism.Services;

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

        public UploadNewPicturePageViewModel(INavigationService navigationService, 
                                             IPageDialogService dialogService) : base(navigationService, dialogService)
        {
        }
    }
}
