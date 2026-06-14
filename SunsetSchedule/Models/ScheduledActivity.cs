using System.ComponentModel.DataAnnotations;

namespace SunsetSchedule.Models;

public class ScheduledActivity
{
    public int Id { get; set; }

    [Required]
    public int ActivityId { get; set; }

    public Activity Activity { get; set; } = default!;

    public string UserId { get; set; } = string.Empty;

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    [StringLength(
        500,
        ErrorMessage = "Notes cannot exceed 500 characters.")]
    public string? Notes { get; set; }
}