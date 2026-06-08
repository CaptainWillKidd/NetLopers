using System.ComponentModel.DataAnnotations;
namespace SunsetSchedule.Models;

public class Activity : IValidatableObject
{
    public int Id { get; set; }

    // Basic Info
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(
    100,
    ErrorMessage = "Name cannot exceed 100 characters.")]
    public string Name { get; set; } = string.Empty;

    // Description is required but not allowed to be super long.
    [Required(ErrorMessage = "Description is required.")]
    [StringLength(
    1000,
    ErrorMessage = "Description cannot exceed 1000 characters.")]
    public string Description { get; set; } = string.Empty;

    // Group size
    [Range(1, 100, ErrorMessage = "Minimum participants must be between 1 and 100.")]
    public int MinParticipants { get; set; }

    [Range(1, 100, ErrorMessage = "Maximum participants must be between 1 and 100.")]
    public int MaxParticipants { get; set; }

    public IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
    {
        if (MinParticipants > MaxParticipants)
        {
            yield return new ValidationResult(
                "Minimum participants cannot exceed maximum participants.",
                new[]
                {
                        nameof(MinParticipants),
                        nameof(MaxParticipants)
                });
        }
    }

    // Categorization (for filtering)
    public ActivityCategory Category { get; set; }

    // Cost filter
    public CostLevel Cost { get; set; }

    // Time
    public TimeOfDay TimeOfDay { get; set; }

    [Required(ErrorMessage = "Duration is required.")]
    [Range(1, 1440, ErrorMessage = "Duration must be between 1 minute and 24 hours.")]
    public int? DurationMinutes { get; set; }

    // Location type
    public ActivityEnvironment Environment { get; set; }

    // Weather compatibility
    public WeatherType AllowedWeather { get; set; }

    // Seasonal availability
    public Season Season { get; set; }

    // Optional location tag
    [StringLength(200)]
    public string? Location { get; set; }

    // Main image shown on details page
    public string? MainImagePath { get; set; }

    // Gallery images
    public ICollection<ActivityImage> Images { get; set; }
        = new List<ActivityImage>();

    // Navigation property
    public ICollection<ScheduledActivity> ScheduledActivities { get; set; }
        = new List<ScheduledActivity>();
}