using Microsoft.EntityFrameworkCore;

namespace Geo_Attendance.Shared.Data
{
    public class AppDbContext : DbContext
    {
        // 1. Put the constructor right here:
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        // 2. Your tables follow right below it:
        public DbSet<AppNotification> Notifications { get; set; }
        public DbSet<ClassSchedule> Schedules { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<AttendanceRecord> AttendanceRecords { get; set; }
        public DateTime? OutTime { get; set; }
    }
}