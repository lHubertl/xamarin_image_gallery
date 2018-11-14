using Rg.Plugins.Popup.Pages;

namespace ImageGallery.Views
{
    public partial class GifPopupPage : PopupPage
    {
        public GifPopupPage()
        {
            InitializeComponent();
        }

        // Prevent hide popup
        protected override bool OnBackButtonPressed() => true;
    }
}
