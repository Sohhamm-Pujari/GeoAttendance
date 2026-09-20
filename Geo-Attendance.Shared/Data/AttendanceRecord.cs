namespace Geo_Attendance.Shared.Data
{
    public class AttendanceRecord
    {
        public int Id { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } // This acts as the IN Time
        public DateTime? OutTime { get; set; }  // Added for Punch Out tracking
        public string Status { get; set; } = string.Empty;
        public string MarkedBy { get; set; } = string.Empty;
    }
    public class AppNotification
    {
        public int Id { get; set; }
        public string SenderName { get; set; } = string.Empty;
        public string TargetId { get; set; } = string.Empty; // "ALL" or specific Student ID (e.g., "S-22")
        public string TargetClass { get; set; } = string.Empty; // e.g., "TYBSc CS"
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }

    public class ClassSchedule
    {
        public int Id { get; set; }
        public string TargetClass { get; set; } = string.Empty; // e.g., "TYBSc CS"
        public string Subject { get; set; } = string.Empty;
        public string Room { get; set; } = string.Empty;
        public DateTime ScheduleDate { get; set; }
        public string FormattedTime { get; set; } = string.Empty;
    }
}
