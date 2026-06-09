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

    // =========================
    // READ OPERATIONS (simple)
    // =========================

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

    public async Task<int> GetScheduledCountAsync(int activityId)
    {
        return await _db.ScheduledActivities
            .CountAsync(sa => sa.ActivityId == activityId);
    }

    // =========================
    // CREATE
    // =========================

    public async Task<ServiceResult<Activity>> CreateAsync(Activity activity)
    {
        try
        {
            _db.Activities.Add(activity);
            await _db.SaveChangesAsync();

            return new ServiceResult<Activity>
            {
                Success = true,
                Data = activity
            };
        }
        catch (DbUpdateException)
        {
            return new ServiceResult<Activity>
            {
                Success = false,
                Error = "Database update failed. Please check your input."
            };
        }
        catch (Exception)
        {
            return new ServiceResult<Activity>
            {
                Success = false,
                Error = "Unexpected error occurred."
            };
        }
    }

    // =========================
    // UPDATE
    // =========================

    public async Task<ServiceResult<Activity>> UpdateAsync(Activity activity)
    {
        try
        {
            var existing = await _db.Activities
                .FirstOrDefaultAsync(a => a.Id == activity.Id);

            if (existing == null)
            {
                return new ServiceResult<Activity>
                {
                    Success = false,
                    Error = "Activity not found."
                };
            }

            _db.Entry(existing).CurrentValues.SetValues(activity);

            await _db.SaveChangesAsync();

            return new ServiceResult<Activity>
            {
                Success = true,
                Data = existing
            };
        }
        catch (DbUpdateException)
        {
            return new ServiceResult<Activity>
            {
                Success = false,
                Error = "Database update failed."
            };
        }
        catch (Exception)
        {
            return new ServiceResult<Activity>
            {
                Success = false,
                Error = "Unexpected error occurred."
            };
        }
    }

    // =========================
    // DELETE
    // =========================

    public async Task<ServiceResult<bool>> DeleteAsync(int id)
    {
        try
        {
            var activity = await _db.Activities
                .Include(a => a.Images)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (activity is null)
            {
                return new ServiceResult<bool>
                {
                    Success = false,
                    Error = "Activity not found.",
                    Data = false
                };
            }

            _db.ActivityImages.RemoveRange(activity.Images);
            _db.Activities.Remove(activity);

            await _db.SaveChangesAsync();

            return new ServiceResult<bool>
            {
                Success = true,
                Data = true
            };
        }
        catch (DbUpdateException)
        {
            return new ServiceResult<bool>
            {
                Success = false,
                Error = "Database delete failed.",
                Data = false
            };
        }
        catch (Exception)
        {
            return new ServiceResult<bool>
            {
                Success = false,
                Error = "Unexpected error occurred.",
                Data = false
            };
        }
    }
}