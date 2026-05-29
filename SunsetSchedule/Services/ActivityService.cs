using SunsetSchedule.Data;
using SunsetSchedule.Models;
using Microsoft.EntityFrameworkCore;

namespace SunsetSchedule.Services;

public class ActivityService
{
    private readonly ApplicationDbContext _db;

    public ActivityService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<Activity>> GetAllAsync()
    {
        return await _db.Activities.ToListAsync();
    }

    public async Task<Activity?> GetByIdAsync(int id)
    {
        return await _db.Activities.FindAsync(id);
    }

    public async Task CreateAsync(Activity activity)
    {
        _db.Activities.Add(activity);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Activity activity)
    {
        _db.Activities.Update(activity);
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
