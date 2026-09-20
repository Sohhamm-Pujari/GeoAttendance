using System;
using System.IO;

namespace Geo_Attendance.Shared
{
    public static class GeofenceConfig
    {
        // Creates a permanent hidden file on the device
        private static string ConfigPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "geofence.cfg");

        // Defaults
        private static double _lat = 17.2777; // Default latitude
        private static double _lon = 73.1818; // Default longitude
        private static int _radius = 50;
        private static bool _isLoaded = false;

        public static double Latitude
        {
            get { EnsureLoaded(); return _lat; }
            set { _lat = value; Save(); }
        }

        public static double Longitude
        {
            get { EnsureLoaded(); return _lon; }
            set { _lon = value; Save(); }
        }

        public static int Radius
        {
            get { EnsureLoaded(); return _radius; }
            set { _radius = value; Save(); }
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
                        double.TryParse(lines[0], out _lat);
                        double.TryParse(lines[1], out _lon);
                        int.TryParse(lines[2], out _radius);
                    }
                }
            }
            catch { /* Ignore read errors and fall back to defaults */ }
            _isLoaded = true;
        }

        private static void Save()
        {
            try
            {
                File.WriteAllLines(ConfigPath, new[] { _lat.ToString(), _lon.ToString(), _radius.ToString() });
            }
            catch { /* Ignore write errors */ }
        }
    }
}