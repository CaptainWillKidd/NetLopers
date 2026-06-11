using System.ComponentModel.DataAnnotations;

namespace SunsetSchedule.Models;

public class LoginRequest
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(256, ErrorMessage = "Password cannot exceed 256 characters.")]
    public string Password { get; set; } = string.Empty;
}
