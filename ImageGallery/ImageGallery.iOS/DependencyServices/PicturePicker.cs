using System;
using System.IO;
using System.Threading.Tasks;
using Foundation;
using ImageGallery.Core.DependencyServices;
using ImageGallery.iOS.DependencyServices;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(PicturePicker))]
namespace ImageGallery.iOS.DependencyServices
{
    public class PicturePicker : IPicturePicker
    {
        TaskCompletionSource<Stream> _taskCompletionSource;
        UIImagePickerController _imagePicker;

        public Task<Stream> GetImageStreamAsync()
        {
            // Create and define UIImagePickerController
            _imagePicker = new UIImagePickerController
            {
                SourceType = UIImagePickerControllerSourceType.PhotoLibrary,
                MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary)
            };

            // Set event handlers
            _imagePicker.FinishedPickingMedia += OnImagePickerFinishedPickingMedia;
            _imagePicker.Canceled += OnImagePickerCancelled;

            // Present UIImagePickerController;
            UIWindow window = UIApplication.SharedApplication.KeyWindow;
            var viewController = window.RootViewController;
            viewController.PresentModalViewController(_imagePicker, true);

            // Return Task object
            _taskCompletionSource = new TaskCompletionSource<Stream>();
            return _taskCompletionSource.Task;
        }

        void OnImagePickerFinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs args)
        {
            UIImage image = args.EditedImage ?? args.OriginalImage;

            if (image != null)
            {
                // Convert UIImage to .NET Stream object
                NSData data = image.AsJPEG(1);
                Stream stream = data.AsStream();

                UnregisterEventHandlers();

                // Set the Stream as the completion of the Task
                _taskCompletionSource.SetResult(stream);
            }
            else
            {
                UnregisterEventHandlers();
                _taskCompletionSource.SetResult(null);
            }
            _imagePicker.DismissModalViewController(true);
        }

        void OnImagePickerCancelled(object sender, EventArgs args)
        {
            UnregisterEventHandlers();
            _taskCompletionSource.SetResult(null);
            _imagePicker.DismissModalViewController(true);
        }

        void UnregisterEventHandlers()
        {
            _imagePicker.FinishedPickingMedia -= OnImagePickerFinishedPickingMedia;
            _imagePicker.Canceled -= OnImagePickerCancelled;
        }
    }
}