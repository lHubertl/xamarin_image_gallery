using System;
using ImageGallery.Core.BusinessLogic.Repositories;
using ImageGallery.Models;
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
                var oauthToken = await SecureStorage.GetAsync(nameof(LoginModel.Token));
                if (!string.IsNullOrEmpty(oauthToken))
                {
                    // Save token to the memory
                    var dataRepository = Container.Resolve<IDataRepository>();
                    dataRepository.Set(DataType.Token, oauthToken);

                    // TODO: navigate to Image page
                    //await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(SignUpPage)}");
                    //return;
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
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpPageViewModel>();
            containerRegistry.RegisterForNavigation<SignInPage, SignInPageViewModel>();

            containerRegistry.RegisterSingleton(typeof(IDataRepository), typeof(DataRepository));

            containerRegistry.Register(typeof(ILoginService), typeof(LoginService));
        }
    }
}
