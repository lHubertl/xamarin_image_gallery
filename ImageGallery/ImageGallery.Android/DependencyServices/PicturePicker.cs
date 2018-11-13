using System.IO;
using System.Threading.Tasks;
using Android.Content;
using ImageGallery.Core.DependencyServices;
using ImageGallery.Droid.DependencyServices;
using Xamarin.Forms;

[assembly: Dependency(typeof(PicturePicker))]
namespace ImageGallery.Droid.DependencyServices
{
    public class PicturePicker : IPicturePicker
    {
        public Task<Stream> GetImageStreamAsync()
        {
            // Define the Intent for getting images
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);

            var activity = MainActivity.Current;

            // Start the picture-picker activity (resumes in MainActivity.cs)
            activity.StartActivityForResult(
                Intent.CreateChooser(intent, "Select Picture"),
                MainActivity.PickImageId);

            // Save the TaskCompletionSource object as a MainActivity property
            activity.PickImageTaskCompletionSource = new TaskCompletionSource<Stream>();

            // Return Task object
            return activity.PickImageTaskCompletionSource.Task;
        }
    }
}