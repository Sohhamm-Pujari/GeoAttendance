using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;
using Geo_Attendance.Shared.Data;

namespace Geo_Attendance.Shared.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private const string USER_SESSION_KEY = "GeoAttendance_EncryptedSession";
        private readonly ISecureStorageService _secureStorage;

        // Inject our contract here!
        public CustomAuthStateProvider(ISecureStorageService secureStorage)
        {
            _secureStorage = secureStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                // Use the interface instead of native MAUI APIs
                string? userJson = await _secureStorage.GetAsync(USER_SESSION_KEY);

                if (string.IsNullOrEmpty(userJson))
                {
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }

                var user = JsonSerializer.Deserialize<AppUser>(userJson);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user!.UniqueId),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("AssignedClass", user.AssignedClass ?? "Unassigned")
                };

                var identity = new ClaimsIdentity(claims, "DeviceSecureAuth");
                var principal = new ClaimsPrincipal(identity);

                return new AuthenticationState(principal);
            }
            catch
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }

        public async Task LogInAsync(AppUser user)
        {
            string userJson = JsonSerializer.Serialize(user);
            await _secureStorage.SetAsync(USER_SESSION_KEY, userJson);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UniqueId),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("AssignedClass", user.AssignedClass ?? "Unassigned")
            };

            var identity = new ClaimsIdentity(claims, "DeviceSecureAuth");
            var principal = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }

        public void LogOut()
        {
            _secureStorage.Remove(USER_SESSION_KEY);
            var principal = new ClaimsPrincipal(new ClaimsIdentity());

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }
    }
}