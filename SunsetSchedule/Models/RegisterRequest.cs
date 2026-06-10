using System.ComponentModel.DataAnnotations;

namespace SunsetSchedule.Models;

public class RegisterRequest
{
    [Required(ErrorMessage = "Username is required.")]
    [StringLength(100, ErrorMessage = "Username cannot exceed 100 characters.")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(256, ErrorMessage = "Password cannot exceed 256 characters.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password confirmation is required.")]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
