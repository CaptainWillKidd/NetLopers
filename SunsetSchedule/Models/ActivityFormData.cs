using Microsoft.AspNetCore.Components.Forms;

namespace SunsetSchedule.Models;

public class ActivityFormData
{
    public Activity Activity { get; set; } = new();

    public IBrowserFile? MainImage { get; set; }
    public IReadOnlyList<IBrowserFile>? GalleryImages { get; set; }
}