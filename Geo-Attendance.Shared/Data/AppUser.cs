using System.ComponentModel.DataAnnotations; // 1. We added this line!

namespace Geo_Attendance.Shared.Data
{
    public class AppUser
    {
        [Key] // 2. This tells the database to use UniqueId as the Primary Key!
        public string UniqueId { get; set; } = "";

        public string Password { get; set; } = "";
        public string Name { get; set; } = "";
        public string Role { get; set; } = "";
        public string AssignedClass { get; set; } = "";
    }
}