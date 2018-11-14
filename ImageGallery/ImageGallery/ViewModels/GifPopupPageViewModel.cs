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

namespace ImageGallery.ViewModels
{
    public class GifPopupPageViewModel : ViewModelBase
    {
        public GifPopupPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
        }
    }
}
