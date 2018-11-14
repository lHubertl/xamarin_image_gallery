using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ImageGallery.Services
{
    public interface IGeolocationService
    {
        Task<Location> GetCurrentLocation();
    }
}
