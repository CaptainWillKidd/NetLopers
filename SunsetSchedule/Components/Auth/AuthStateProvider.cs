using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using SunsetSchedule.Models;
using SunsetSchedule.Services;
using System.Security.Claims;

namespace SunsetSchedule.Components;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly AuthService _authService;
    private readonly ProtectedSessionStorage _sessionStorage;
    private User? _cachedUser;

    public event Action? OnUserChanged;

    public AuthStateProvider(AuthService authService, ProtectedSessionStorage sessionStorage)
    {
        _authService = authService;
        _sessionStorage = sessionStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var userResult = await _sessionStorage.GetAsync<User>("currentUser");
            
            if (userResult.Success && userResult.Value != null)
            {
                _cachedUser = userResult.Value;
                return CreateAuthState(userResult.Value);
            }
        }
        catch (InvalidOperationException)
        {
            // ProtectedSessionStorage only works in interactive mode
            if (_cachedUser != null)
            {
                return CreateAuthState(_cachedUser);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Auth error: {ex.Message}");
        }

        return new AuthenticationState(new ClaimsPrincipal());
    }

    private AuthenticationState CreateAuthState(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var identity = new ClaimsIdentity(claims, "auth");
        var principal = new ClaimsPrincipal(identity);
        
        return new AuthenticationState(principal);
    }

    public async Task SetUserAsync(User? user)
    {
        try
        {
            _cachedUser = user;
            
            if (user != null)
            {
                await _sessionStorage.SetAsync("currentUser", user);
            }
            else
            {
                await _sessionStorage.DeleteAsync("currentUser");
            }

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            OnUserChanged?.Invoke();
        }
        catch (InvalidOperationException)
        {
            // ProtectedSessionStorage can only be used in interactive mode
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            OnUserChanged?.Invoke();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"SetUser error: {ex.Message}");
        }
    }

    public async Task<User?> GetCurrentUserAsync()
    {
        try
        {
            if (_cachedUser != null)
                return _cachedUser;

            var userResult = await _sessionStorage.GetAsync<User>("currentUser");
            if (userResult.Success && userResult.Value != null)
            {
                _cachedUser = userResult.Value;
                return userResult.Value;
            }
        }
        catch (InvalidOperationException)
        {
            return _cachedUser;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"GetCurrentUser error: {ex.Message}");
        }
        
        return _cachedUser;
    }

    public async Task LogoutAsync()
    {
        try
        {
            _cachedUser = null;
            await _sessionStorage.DeleteAsync("currentUser");
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            OnUserChanged?.Invoke();
        }
        catch (InvalidOperationException)
        {
            // ProtectedSessionStorage can only be used in interactive mode
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            OnUserChanged?.Invoke();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Logout error: {ex.Message}");
        }
    }
}
