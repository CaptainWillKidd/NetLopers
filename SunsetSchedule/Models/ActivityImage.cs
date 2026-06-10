namespace SunsetSchedule.Models;

public class ActivityImage
{
    public int Id { get; set; }

    public int ActivityId { get; set; }

    public string ImagePath { get; set; } = string.Empty;

    public string? Caption { get; set; }

    public int DisplayOrder { get; set; }

    public Activity Activity { get; set; } = null!;
}