namespace SunsetSchedule.Models;

public class Activity
{
    public int Id { get; set; }

    // Basic Info
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    // Categorization (for filtering)
    public ActivityCategory Category { get; set; }

    // Cost filter
    public CostLevel Cost { get; set; }

    // Group size (numeric filtering - more flexible)
    public int MinParticipants { get; set; }
    public int MaxParticipants { get; set; }

    // Time
    public TimeOfDay TimeOfDay { get; set; }
    public int? DurationMinutes { get; set; }

    // Location type
    public ActivityEnvironment Environment { get; set; }

    // Weather compatibility (flags recommended for filtering)
    public WeatherType AllowedWeather { get; set; }

    // Seasonal availability
    public Season Season { get; set; }

    // Optional location tag
    public string? Location { get; set; }

    // Navigation property
    public ICollection<ScheduledActivity> ScheduledActivities { get; set; }
        = new List<ScheduledActivity>();
}