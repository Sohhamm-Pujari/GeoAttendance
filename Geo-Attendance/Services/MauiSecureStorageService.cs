using Geo_Attendance.Shared.Services;
using Microsoft.Maui.Storage;

namespace Geo_Attendance.Services
{
    public class MauiSecureStorageService : ISecureStorageService
    {
        public Task<string?> GetAsync(string key)
        {
            return SecureStorage.Default.GetAsync(key);
        }

        public Task SetAsync(string key, string value)
        {
            return SecureStorage.Default.SetAsync(key, value);
        }

        public void Remove(string key)
        {
            SecureStorage.Default.Remove(key);
        }
    }
}