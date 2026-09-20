using Geo_Attendance.Shared.Data;
namespace Geo_Attendance.Shared
{
    public static class GlobalState
    {
        public static AppUser? CurrentUser { get; set; }
        public static void Logout() => CurrentUser = null;
    }
}