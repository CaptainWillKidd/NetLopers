namespace SunsetSchedule.Models;

public class ScheduledActivity
{
    public int Id { get; set; }

    public int ActivityId { get; set; }
    public Activity Activity { get; set; } = default!;

    public string UserId { get; set; } = string.Empty;

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? Notes { get; set; }
}