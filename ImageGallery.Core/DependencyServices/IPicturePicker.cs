using System.IO;
using System.Threading.Tasks;

namespace ImageGallery.Core.DependencyServices
{
    public interface IPicturePicker
    {
        Task<Stream> GetImageStreamAsync();
    }
}
