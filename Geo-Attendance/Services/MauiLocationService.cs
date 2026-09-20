using Geo_Attendance.Shared.Services;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.ApplicationModel;

namespace Geo_Attendance.Services
{
    public class MauiLocationService : ILocationService
    {
        public async Task<GeoLocationResult> GetCurrentLocationAsync()
        {
            var result = new GeoLocationResult();

            try
            {
                // Request high-accuracy GPS from the hardware
                var request = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(10));
                var location = await Geolocation.Default.GetLocationAsync(request);

                if (location != null)
                {
                    result.Latitude = location.Latitude;
                    result.Longitude = location.Longitude;
                    result.IsSuccess = true;
                }
                else
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Signal Lost: Unable to lock onto GPS satellites.";
                }
            }
            catch (FeatureNotSupportedException) { result.ErrorMessage = "GPS hardware is not supported on this device."; }
            catch (FeatureNotEnabledException) { result.ErrorMessage = "Your device's GPS is turned off. Please enable it in settings."; }
            catch (PermissionException) { result.ErrorMessage = "Location permission denied. Please allow GeoAttendance to access your location."; }
            catch (Exception ex) { result.ErrorMessage = $"System Error: {ex.Message}"; }

            return result;
        }
    }
}