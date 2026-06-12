using Microsoft.EntityFrameworkCore;
using SunsetSchedule.Data;
using SunsetSchedule.Models;

namespace SunsetSchedule.Services;

public class ScheduledActivityService
{
    private readonly ApplicationDbContext _db;

    public ScheduledActivityService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<ScheduledActivity>> GetAllAsync()
    {
        return await _db.ScheduledActivities
            .Include(sa => sa.Activity)
            .ToListAsync();
    }

    public async Task<ScheduledActivity?> GetByIdAsync(int id)
    {
        return await _db.ScheduledActivities
            .Include(sa => sa.Activity)
            .FirstOrDefaultAsync(sa => sa.Id == id);
    }

    public async Task CreateAsync(ScheduledActivity scheduledActivity)
    {
        scheduledActivity.StartTime = DateTime.SpecifyKind(
            scheduledActivity.StartTime,
            DateTimeKind.Utc
        );

        if (scheduledActivity.EndTime.HasValue)
        {
            scheduledActivity.EndTime = DateTime.SpecifyKind(
                scheduledActivity.EndTime.Value,
                DateTimeKind.Utc
            );
        }

        _db.ScheduledActivities.Add(scheduledActivity);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(ScheduledActivity scheduledActivity)
    {
        _db.ScheduledActivities.Update(scheduledActivity);

        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var scheduledActivity =
            await _db.ScheduledActivities.FindAsync(id);

        if (scheduledActivity is null) return;

        _db.ScheduledActivities.Remove(scheduledActivity);

        await _db.SaveChangesAsync();
    }

    public async Task<List<ScheduledActivity>> GetAllByActivityIdAsync(int activityId)
    {
        return await _db.ScheduledActivities
            .Where(sa => sa.ActivityId == activityId)
            .OrderBy(sa => sa.StartTime)
            .ToListAsync();
    }
    
}