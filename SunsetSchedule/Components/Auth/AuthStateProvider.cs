using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using SunsetSchedule.Models;
using System.Security.Claims;

namespace SunsetSchedule.Components;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly ProtectedSessionStorage _sessionStorage;
    private User? _cachedUser;

    public event Action? OnUserChanged;

    public AuthStateProvider(ProtectedSessionStorage sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // 1. fast memory check
        if (_cachedUser != null)
            return CreateAuthState(_cachedUser);

        try
        {
            // 2. session storage check
            var result = await _sessionStorage.GetAsync<User>("currentUser");

            if (result.Success && result.Value != null)
            {
                _cachedUser = result.Value;
                return CreateAuthState(_cachedUser);
            }
        }
        catch
        {
            // prerender / JS not ready → treat as logged out
        }

        return Anonymous();
    }

    public async Task SetUserAsync(User? user)
    {
        _cachedUser = user;

        try
        {
            if (user == null)
                await _sessionStorage.DeleteAsync("currentUser");
            else
                await _sessionStorage.SetAsync("currentUser", user);
        }
        catch
        {
            // ignore prerender issues
        }

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        OnUserChanged?.Invoke();
    }

    public async Task<User?> GetCurrentUserAsync()
    {
        if (_cachedUser != null)
            return _cachedUser;

        try
        {
            var result = await _sessionStorage.GetAsync<User>("currentUser");

            if (result.Success && result.Value != null)
                _cachedUser = result.Value;
        }
        catch
        {
            // ignore prerender issues
        }

        return _cachedUser;
    }

    public async Task LogoutAsync()
    {
        _cachedUser = null;

        try
        {
            await _sessionStorage.DeleteAsync("currentUser");
        }
        catch { }

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        OnUserChanged?.Invoke();
    }

    private AuthenticationState CreateAuthState(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var identity = new ClaimsIdentity(claims, "manual-session");
        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    private AuthenticationState Anonymous()
    {
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }
}