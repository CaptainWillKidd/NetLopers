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

    //===================================
    // RANDOM ACTIVITY GENERATOR
    //===================================
    private readonly Random _random = new();

    /// <summary>
    /// Get a single random activity (no filters)
    /// </summary>
    public async Task<Activity?> GetRandomActivityAsync()
    {
        var all = await GetAllAsync();
        if (!all.Any()) return null;

        var randomIndex = _random.Next(all.Count);
        return all[randomIndex];
    }

    /// <summary>
    /// Get filtered activities based on user preferences
    /// </summary>
    public async Task<List<Activity>> GetFilteredActivitiesAsync(
        ActivityEnvironment? environment = null,
        CostLevel? cost = null,
        WeatherType? weather = null,
        TimeOfDay? timeOfDay = null,
        ActivityCategory? category = null,
        Season? season = null,
        int? groupSize = null
    )
    {
        var query = _db.Activities
            .Include(a => a.Images)
            .AsQueryable();
        // Environment filter (Indoor/Outdoor/Either)
        if (environment.HasValue && environment != ActivityEnvironment.Either)
        {
            query = query.Where(a => a.Environment == environment.Value);
        }

                // Cost filter
        if (cost.HasValue)
        {
            query = query.Where(a => a.Cost == cost.Value);
        }

        // Weather filter (match exact or "Any")
        if (weather.HasValue && weather != WeatherType.Any)
        {
            query = query.Where(a => a.AllowedWeather == weather.Value 
                || a.AllowedWeather == WeatherType.Any);
        }

        // Time of day filter
        if (timeOfDay.HasValue && timeOfDay != TimeOfDay.Anytime)
        {
            query = query.Where(a => a.TimeOfDay == timeOfDay.Value);
        }

        // Category filter
        if (category.HasValue)
        {
            query = query.Where(a => a.Category == category.Value);
        }

        // Season filter
        if (season.HasValue && season != Season.Any)
        {
            query = query.Where(a => a.Season == season.Value 
                || a.Season == Season.Any);
        }

        // Group size filter (must fit within min/max participants)
        if (groupSize.HasValue)
        {
            query = query.Where(a => a.MinParticipants <= groupSize.Value 
                && a.MaxParticipants >= groupSize.Value);
        }

        return await query.ToListAsync();
    }

    /// <summary>
    /// Get a random activity that matches the specified filters
    /// </summary>
    public async Task<Activity?> GetRandomFilteredActivityAsync(
        ActivityEnvironment? environment = null,
        CostLevel? cost = null,
        WeatherType? weather = null,
        TimeOfDay? timeOfDay = null,
        ActivityCategory? category = null,
        Season? season = null,
        int? groupSize = null
    )
    {
        var filtered = await GetFilteredActivitiesAsync(
            environment, cost, weather, timeOfDay, category, season, groupSize
        );

        if(!filtered.Any()) return null;
        
        var randomIndex = _random.Next(filtered.Count);
        return filtered[randomIndex];
    }

    /// <summary>
    /// Get a random activity with filters, returns null with reason if no matches
    /// </summary>
    public async Task<(Activity? Activity, string Message)> GetRandomActivityWithMessageAsync(
        ActivityEnvironment? environment = null,
        CostLevel? cost = null,
        WeatherType? weather = null,
        TimeOfDay? timeOfDay = null,
        ActivityCategory? category = null,
        Season? season = null,
        int? groupSize = null)
    {
        var filtered = await GetFilteredActivitiesAsync(
            environment, cost, weather, timeOfDay, category, season, groupSize);

        if (!filtered.Any())
        {
            return (null, "No activities match your filters. Try adjusting your criteria!");
        }

        var randomIndex = _random.Next(filtered.Count);
        return (filtered[randomIndex], "Activity found!");
    }

    /// <summary>
    /// Get distinct filter options for UI dropdowns (based on actual data)
    /// </summary>
    public async Task<FilterOptions> GetFilterOptionAsync()
    {
        var allActivities = await GetAllAsync();
        
        return new FilterOptions
        {
            Environments = allActivities.Select(a => a.Environment).Distinct().ToList(),
            Costs = allActivities.Select(a => a.Cost).Distinct().ToList(),
            WeatherTypes = allActivities.Select(a => a.AllowedWeather).Distinct().ToList(),
            TimesOfDay = allActivities.Select(a => a.TimeOfDay).Distinct().ToList(),
            Categories = allActivities.Select(a => a.Category).Distinct().ToList(),
            Seasons = allActivities.Select(a => a.Season).Distinct().ToList(),
            MinGroupSize = allActivities.Min(a => a.MinParticipants),
            MaxGroupSize = allActivities.Max(a => a.MaxParticipants)
        };
        
    }

    
    






}

public class FilterOptions
{
    public List<ActivityEnvironment> Environments { get; set; } = new();
    public List<CostLevel> Costs { get; set; } = new();
    public List<WeatherType> WeatherTypes { get; set; } = new();
    public List<TimeOfDay> TimesOfDay { get; set; } = new();
    public List<ActivityCategory> Categories { get; set; } = new();
    public List<Season> Seasons { get; set; } = new();
    public int MinGroupSize { get; set; }
    public int MaxGroupSize { get; set; }
}