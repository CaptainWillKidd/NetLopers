using SunsetSchedule.Data;
using SunsetSchedule.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Forms;

namespace SunsetSchedule.Services;

public class ActivityService
{
    private readonly ApplicationDbContext _db;
    private readonly ImageService _imageService;

    public ActivityService(ApplicationDbContext db, ImageService imageService)
    {
        _db = db;
        _imageService = imageService;
    }

    public async Task<List<Activity>> GetAllAsync()
    {
        return await _db.Activities
            .Include(a => a.Images)
            .ToListAsync();
    }

    public async Task<Activity?> GetByIdAsync(int id)
    {
        return await _db.Activities
            .Include(a => a.Images)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Activity> CreateAsync(Activity activity)
    {
        _db.Activities.Add(activity);
        await _db.SaveChangesAsync();

        return activity;
    }

    public async Task UpdateAsync(Activity activity, IBrowserFile? mainImage, IReadOnlyList<IBrowserFile>? gallery)
    {
        _db.Activities.Update(activity);
        await _db.SaveChangesAsync();

        // MAIN IMAGE
        if (mainImage is not null)
        {
            var path = await _imageService.SaveImageAsync(mainImage, "wwwroot/images/uploads");

            _db.ActivityImages.Add(new ActivityImage
            {
                ActivityId = activity.Id,
                ImagePath = path,
                DisplayOrder = 0
            });
        }

        // GALLERY IMAGES
        if (gallery is not null)
        {
            int order = 1;

            foreach (var file in gallery)
            {
                var path = await _imageService.SaveImageAsync(file, "wwwroot/images/uploads");

                _db.ActivityImages.Add(new ActivityImage
                {
                    ActivityId = activity.Id,
                    ImagePath = path,
                    DisplayOrder = order++
                });
            }
        }

        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var activity = await _db.Activities.FindAsync(id);

        if (activity is null) return;

        _db.Activities.Remove(activity);

        await _db.SaveChangesAsync();
    }

    public async Task<int> GetScheduledCountAsync(int activityId)
    {
        return await _db.ScheduledActivities
            .CountAsync(sa => sa.ActivityId == activityId);
    }
}
