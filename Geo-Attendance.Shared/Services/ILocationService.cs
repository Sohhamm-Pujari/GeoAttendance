namespace Geo_Attendance.Shared.Services
{
    // A simple object to pass the coordinates back to the UI safely
    public class GeoLocationResult
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
    }

    // The contract
    public interface ILocationService
    {
        Task<GeoLocationResult> GetCurrentLocationAsync();
    }
}