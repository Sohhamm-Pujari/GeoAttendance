using System;
using System.IO;

namespace Geo_Attendance.Shared
{
    public static class AcademicConfig
    {
        // Creates a permanent hidden file on the device
        private static string ConfigPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "academic.cfg");

        private static DateTime _start = new DateTime(2026, 6, 1);
        private static DateTime _end = new DateTime(2026, 11, 30);
        private static string _holidays = "2026-08-15";
        private static bool _isLoaded = false;

        public static DateTime StartDate
        {
            get { EnsureLoaded(); return _start; }
            set { _start = value; Save(); }
        }

        public static DateTime EndDate
        {
            get { EnsureLoaded(); return _end; }
            set { _end = value; Save(); }
        }

        public static string Holidays
        {
            get { EnsureLoaded(); return _holidays; }
            set { _holidays = value; Save(); }
        }

        private static void EnsureLoaded()
        {
            if (_isLoaded) return;
            try
            {
                if (File.Exists(ConfigPath))
                {
                    var lines = File.ReadAllLines(ConfigPath);
                    if (lines.Length >= 3)
                    {
                        DateTime.TryParse(lines[0], out _start);
                        DateTime.TryParse(lines[1], out _end);
                        _holidays = lines[2];
                    }
                }
            }
            catch { }
            _isLoaded = true;
        }

        private static void Save()
        {
            try
            {
                File.WriteAllLines(ConfigPath, new[] { _start.ToString("yyyy-MM-dd"), _end.ToString("yyyy-MM-dd"), _holidays });
            }
            catch { }
        }
    }
}