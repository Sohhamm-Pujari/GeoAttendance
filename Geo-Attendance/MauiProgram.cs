using Microsoft.AspNetCore.Components.Authorization;
using Geo_Attendance.Services;
using Geo_Attendance.Shared.Services;
using Geo_Attendance.Shared.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Geo_Attendance
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Add device-specific services used by the Geo_Attendance.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();
            builder.Services.AddSingleton<ILocationService, MauiLocationService>();
            builder.Services.AddSingleton<ISecureStorageService, MauiSecureStorageService>();

            // Configure SQLite Database path safely for your device/desktop
            // ---> CHANGED TO V3 HERE <---
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "GeoAttendance_V3.db");
            System.Diagnostics.Debug.WriteLine($"\n\n=== YOUR DATABASE IS HERE: {dbPath} ===\n\n");

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite($"Data Source={dbPath}"));

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            // ==========================================
            // PRODUCTION SECURITY & STATE MANAGEMENT
            // ==========================================
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

            return builder.Build();
        }
    }
}