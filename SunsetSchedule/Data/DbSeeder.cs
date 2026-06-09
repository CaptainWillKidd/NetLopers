using SunsetSchedule.Models;

namespace SunsetSchedule.Data;

public static class DbSeeder
{
    public static void Seed(ApplicationDbContext db)
    {
        // ----------------------------
        // SEED ACTIVITIES
        // ----------------------------
        if (!db.Activities.Any())
        {
            db.Activities.AddRange(
                new Activity
                {
                    Name = "Hike Bridal Veil Falls",
                    Description = "Easy scenic waterfall hike in Provo Canyon.",
                    Category = ActivityCategory.Outdoor,
                    Cost = CostLevel.Free,
                    MinParticipants = 1,
                    MaxParticipants = 8,
                    TimeOfDay = TimeOfDay.Morning,
                    DurationMinutes = 120,
                    Environment = ActivityEnvironment.Outdoor,
                    AllowedWeather = WeatherType.Sunny,
                    Season = Season.Spring,
                    Location = "Provo Canyon"
                },

                new Activity
                {
                    Name = "Board Game Night",
                    Description = "Family board game evening at home.",
                    Category = ActivityCategory.Social,
                    Cost = CostLevel.Free,
                    MinParticipants = 2,
                    MaxParticipants = 10,
                    TimeOfDay = TimeOfDay.Evening,
                    DurationMinutes = 180,
                    Environment = ActivityEnvironment.Indoor,
                    AllowedWeather = WeatherType.Any,
                    Season = Season.Any,
                    Location = "Home"
                },

                new Activity
                {
                    Name = "Thanksgiving Point Museum",
                    Description = "Explore museums and exhibits.",
                    Category = ActivityCategory.Entertainment,
                    Cost = CostLevel.Medium,
                    MinParticipants = 1,
                    MaxParticipants = 12,
                    TimeOfDay = TimeOfDay.Afternoon,
                    DurationMinutes = 240,
                    Environment = ActivityEnvironment.Indoor,
                    AllowedWeather = WeatherType.Any,
                    Season = Season.Any,
                    Location = "Lehi"
                },

                new Activity
                {
                    Name = "Drive Through Alpine Loop",
                    Description = "Scenic mountain drive with fall colors.",
                    Category = ActivityCategory.Outdoor,
                    Cost = CostLevel.Low,
                    MinParticipants = 1,
                    MaxParticipants = 6,
                    TimeOfDay = TimeOfDay.Afternoon,
                    DurationMinutes = 180,
                    Environment = ActivityEnvironment.Outdoor,
                    AllowedWeather = WeatherType.Sunny,
                    Season = Season.Fall,
                    Location = "American Fork Canyon"
                },

                new Activity
                {
                    Name = "Temple Square Lights",
                    Description = "Walk through holiday light displays.",
                    Category = ActivityCategory.Entertainment,
                    Cost = CostLevel.Free,
                    MinParticipants = 1,
                    MaxParticipants = 20,
                    TimeOfDay = TimeOfDay.Evening,
                    DurationMinutes = 90,
                    Environment = ActivityEnvironment.Outdoor,
                    AllowedWeather = WeatherType.Cold,
                    Season = Season.Winter,
                    Location = "Salt Lake City"
                }
            );

            db.SaveChanges();

            if (!db.ActivityImages.Any())
            {
                var activities = db.Activities.ToDictionary(a => a.Name);

                db.ActivityImages.AddRange(
                    new ActivityImage
                    {
                        ActivityId = activities["Hike Bridal Veil Falls"].Id,
                        ImagePath = "/images/bridalveilfalls.webp",
                        DisplayOrder = 1
                    },
                    new ActivityImage
                    {
                        ActivityId = activities["Hike Bridal Veil Falls"].Id,
                        ImagePath = "/images/bridalveilfalls2.webp",
                        DisplayOrder = 2
                    },
                    new ActivityImage
                    {
                        ActivityId = activities["Hike Bridal Veil Falls"].Id,
                        ImagePath = "/images/bridalveilfalls3.webp",
                        DisplayOrder = 2
                    },
                    new ActivityImage
                    {
                        ActivityId = activities["Board Game Night"].Id,
                        ImagePath = "/images/board_gaming.webp",
                        DisplayOrder = 1
                    },
                    new ActivityImage
                    {
                        ActivityId = activities["Board Game Night"].Id,
                        ImagePath = "/images/board_game01.webp",
                        DisplayOrder = 2
                    },
                    new ActivityImage
                    {
                        ActivityId = activities["Board Game Night"].Id,
                        ImagePath = "/images/board_game02.webp",
                        DisplayOrder = 3
                    },
                    new ActivityImage
                    {
                        ActivityId = activities["Board Game Night"].Id,
                        ImagePath = "/images/board_game03.webp",
                        DisplayOrder = 4
                    },
                    new ActivityImage
                    {
                        ActivityId = activities["Thanksgiving Point Museum"].Id,
                        ImagePath = "/images/thanksgiving_point01.webp",
                        DisplayOrder = 1
                    },
                    new ActivityImage
                    {
                        ActivityId = activities["Drive Through Alpine Loop"].Id,
                        ImagePath = "/images/alpine_loop01.webp",
                        DisplayOrder = 1
                    },
                    new ActivityImage
                    {
                        ActivityId = activities["Drive Through Alpine Loop"].Id,
                        ImagePath = "/images/alpine_loop02.webp",
                        DisplayOrder = 2
                    },
                    new ActivityImage
                    {
                        ActivityId = activities["Temple Square Lights"].Id,
                        ImagePath = "/images/temple_square_lights.webp",
                        DisplayOrder = 1
                    },
                    new ActivityImage
                    {
                        ActivityId = activities["Temple Square Lights"].Id,
                        ImagePath = "/images/temple_square_lights02.webp",
                        DisplayOrder = 1
                    }
                );

                db.SaveChanges();
            }
        }

        // ----------------------------
        // SEED SCHEDULED ACTIVITIES
        // ----------------------------
        if (!db.ScheduledActivities.Any())
        {
            var activities = db.Activities.ToDictionary(a => a.Name);

            db.ScheduledActivities.AddRange(
                new ScheduledActivity
                {
                    ActivityId = activities["Hike Bridal Veil Falls"].Id,
                    UserId = "seed-user-1",
                    StartTime = DateTime.UtcNow.AddDays(1).AddHours(8),
                    EndTime = DateTime.UtcNow.AddDays(1).AddHours(10),
                    Notes = "Morning hike with family"
                },

                new ScheduledActivity
                {
                    ActivityId = activities["Board Game Night"].Id,
                    UserId = "seed-user-2",
                    StartTime = DateTime.UtcNow.AddDays(2).AddHours(18),
                    EndTime = DateTime.UtcNow.AddDays(2).AddHours(21),
                    Notes = "Bring snacks and new games"
                },

                new ScheduledActivity
                {
                    ActivityId = activities["Thanksgiving Point Museum"].Id,
                    UserId = "seed-user-1",
                    StartTime = DateTime.UtcNow.AddDays(3).AddHours(13),
                    EndTime = DateTime.UtcNow.AddDays(3).AddHours(17),
                    Notes = "Plan for lunch beforehand"
                },

                new ScheduledActivity
                {
                    ActivityId = activities["Temple Square Lights"].Id,
                    UserId = "seed-user-3",
                    StartTime = DateTime.UtcNow.AddDays(4).AddHours(19),
                    EndTime = DateTime.UtcNow.AddDays(4).AddHours(20),
                    Notes = "Evening walk, dress warm"
                }
            );

            db.SaveChanges();
        }
    }
}