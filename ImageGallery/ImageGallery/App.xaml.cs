using System;
using System.Threading.Tasks;
using ImageGallery.Core.BusinessLogic.Repositories;
using ImageGallery.Services;
using Prism;
using Prism.Ioc;
using ImageGallery.ViewModels;
using ImageGallery.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Plugin.Popups;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ImageGallery
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            try
            {
                var oauthToken = await SecureStorage.GetAsync(DataType.Token.ToString());
                if (!string.IsNullOrEmpty(oauthToken))
                {
                    await FillDataRepository(oauthToken);

                    await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(ImagesPage)}");
                    return;
                }
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
            }

            await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(SignUpPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterPopupNavigationService();
            containerRegistry.RegisterForNavigation<GifPopupPage>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpPageViewModel>();
            containerRegistry.RegisterForNavigation<SignInPage, SignInPageViewModel>();

            containerRegistry.RegisterSingleton(typeof(IDataRepository), typeof(DataRepository));

            containerRegistry.Register(typeof(ILoginService), typeof(LoginService));
            containerRegistry.Register(typeof(IImageService), typeof(ImageService));
            containerRegistry.Register(typeof(IGeolocationService), typeof(GeolocationService));
            containerRegistry.RegisterForNavigation<ImagesPage, ImagesPageViewModel>();

            containerRegistry.RegisterForNavigation<UploadNewPicturePage, UploadNewPicturePageViewModel>();
        }

        /// <summary>
        /// Save user data to the memory
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task FillDataRepository(string token)
        {
            var dataRepository = Container.Resolve<IDataRepository>();

            // Save user data to the memory
            var avatar = await SecureStorage.GetAsync(DataType.AvatarUrl.ToString());

            dataRepository.Set(DataType.Token, token);
            dataRepository.Set(DataType.AvatarUrl, avatar);
        }
    }
}
