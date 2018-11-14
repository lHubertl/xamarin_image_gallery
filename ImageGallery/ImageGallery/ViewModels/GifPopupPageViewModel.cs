using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Logging;
using Prism.Services;
using Prism.Mvvm;
using ImageGallery.Core.Infrastructure;
using ImageGallery.Services;
using ImageGallery.Constants;

namespace ImageGallery.ViewModels
{
    public class GifPopupPageViewModel : ViewModelBase
    {

        private string _gif;
        public string Gif
        {
            get => _gif;
            set => SetProperty(ref _gif, value);
        }
        public GifPopupPageViewModel(INavigationService navigationService, 
                                     IPageDialogService dialogService) : base(navigationService, dialogService)
        {
        }
        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            if(parameters != null)
            {
                if(parameters.TryGetValue<string>(NavParamKeys.GifUrl, out string url))
                {
                    Gif = url;
                }
            }
        }
    }
}
