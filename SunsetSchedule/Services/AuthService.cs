using SunsetSchedule.Data;
using SunsetSchedule.Models;
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

    /// <summary>
    /// Registers a new user
    /// </summary>
    public async Task<ServiceResult<User>> RegisterAsync(RegisterRequest request)
    {
        try
        {
            // Check if email already exists
            var existingEmailUser = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            if (existingEmailUser != null)
                return ServiceResult<User>.FailureResult("Email is already registered.");

            // Check if username already exists
            var existingUsernameUser = _context.Users.FirstOrDefault(u => u.Username == request.Username);
            if (existingUsernameUser != null)
                return ServiceResult<User>.FailureResult("Username is already taken.");

            // Create new user with hashed password
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

    /// <summary>
    /// Authenticates a user with email and password
    /// </summary>
    public async Task<ServiceResult<User>> LoginAsync(LoginRequest request)
    {
        try
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email && u.IsActive);
            
            if (user == null)
                return ServiceResult<User>.FailureResult("Invalid email or password.");

            if (!VerifyPassword(request.Password, user.PasswordHash))
                return ServiceResult<User>.FailureResult("Invalid email or password.");

            // Update last login
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

    /// <summary>
    /// Gets a user by email
    /// </summary>
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await Task.FromResult(_context.Users.FirstOrDefault(u => u.Email == email && u.IsActive));
    }

    /// <summary>
    /// Logs out the current user
    /// </summary>
    public async Task LogoutAsync()
    {
        // Currently just marks as logged out via authentication state
        // In future, could add logging or token invalidation
        await Task.CompletedTask;
    }

    /// <summary>
    /// Hashes a password using SHA256
    /// </summary>
    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }

    /// <summary>
    /// Verifies a password against its hash
    /// </summary>
    private bool VerifyPassword(string password, string hash)
    {
        var hashOfInput = HashPassword(password);
        return hashOfInput == hash;
    }
}
