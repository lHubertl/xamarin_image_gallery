using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ImageGallery.Services
{
    public class GeolocationService : IGeolocationService
    {
        public async Task<Location> GetCurrentLocation()
        {
            Location location = null;
            Location harcodedLocation = new Location(23.908023, 49.921119);

            try
            {
                location = await Geolocation.GetLastKnownLocationAsync();
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // TODO: Handle not supported on device exception
                location = harcodedLocation;
            }
            catch (PermissionException pEx)
            {
                // TODO: Handle permission exception
                location = harcodedLocation;
            }
            catch (Exception ex)
            {
                // TODO: Unable to get location
                location = harcodedLocation;
            }

            if (location == null)
            {
                location = harcodedLocation;
            }

            return location;
        }
    }
}
