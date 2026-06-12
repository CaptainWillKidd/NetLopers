using SunsetSchedule.Data;
using SunsetSchedule.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace SunsetSchedule.Services;

public class AuthService
{
    private readonly ApplicationDbContext _context;

    public AuthService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<User>> RegisterAsync(RegisterRequest request)
    {
        try
        {
            var existingEmailUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (existingEmailUser != null)
                return ServiceResult<User>.FailureResult("Email is already registered.");

            var existingUsernameUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.Username);

            if (existingUsernameUser != null)
                return ServiceResult<User>.FailureResult("Username is already taken.");

            var user = new User
            {
                Email = request.Email,
                Username = request.Username,
                PasswordHash = HashPassword(request.Password),
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return ServiceResult<User>.SuccessResult(user);
        }
        catch (Exception ex)
        {
            return ServiceResult<User>.FailureResult($"Registration failed: {ex.Message}");
        }
    }

    public async Task<ServiceResult<User>> LoginAsync(LoginRequest request)
    {
        try
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email && u.IsActive);

            if (user == null)
                return ServiceResult<User>.FailureResult("Invalid email or password.");

            if (!VerifyPassword(request.Password, user.PasswordHash))
                return ServiceResult<User>.FailureResult("Invalid email or password.");

            user.LastLogin = DateTime.UtcNow;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return ServiceResult<User>.SuccessResult(user);
        }
        catch (Exception ex)
        {
            return ServiceResult<User>.FailureResult($"Login failed: {ex.Message}");
        }
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email && u.IsActive);
    }

    public Task LogoutAsync()
    {
        return Task.CompletedTask;
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    private bool VerifyPassword(string password, string hash)
    {
        return HashPassword(password) == hash;
    }
}