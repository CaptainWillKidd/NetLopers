using SunsetSchedule.Models;

namespace SunsetSchedule.Data;

public static class DbSeeder
{
    public static void Seed(ApplicationDbContext db)
    {
        if (db.Activities.Any())
        {
            return; // Don't reseed if data exists
        }

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
            },

            new Activity
            {
                Name = "Hot Chocolate Date Night",
                Description = "Visit a local café for desserts and drinks.",
                Category = ActivityCategory.Food,
                Cost = CostLevel.Low,
                MinParticipants = 2,
                MaxParticipants = 4,
                TimeOfDay = TimeOfDay.Evening,
                DurationMinutes = 90,
                Environment = ActivityEnvironment.Indoor,
                AllowedWeather = WeatherType.Any,
                Season = Season.Any,
                Location = "Orem"
            },

            new Activity
            {
                Name = "Bean Museum Visit",
                Description = "Explore wildlife exhibits at BYU.",
                Category = ActivityCategory.Educational,
                Cost = CostLevel.Free,
                MinParticipants = 1,
                MaxParticipants = 15,
                TimeOfDay = TimeOfDay.Afternoon,
                DurationMinutes = 120,
                Environment = ActivityEnvironment.Indoor,
                AllowedWeather = WeatherType.Any,
                Season = Season.Any,
                Location = "Provo"
            },

            new Activity
            {
                Name = "Snowshoeing at Sundance",
                Description = "Winter trail adventure in the mountains.",
                Category = ActivityCategory.Exercise,
                Cost = CostLevel.Medium,
                MinParticipants = 1,
                MaxParticipants = 6,
                TimeOfDay = TimeOfDay.Morning,
                DurationMinutes = 240,
                Environment = ActivityEnvironment.Outdoor,
                AllowedWeather = WeatherType.Snowy,
                Season = Season.Winter,
                Location = "Sundance"
            },

            new Activity
            {
                Name = "Picnic at Canyon Glen Park",
                Description = "Relaxing outdoor picnic by the river.",
                Category = ActivityCategory.Relaxation,
                Cost = CostLevel.Low,
                MinParticipants = 2,
                MaxParticipants = 12,
                TimeOfDay = TimeOfDay.Afternoon,
                DurationMinutes = 150,
                Environment = ActivityEnvironment.Outdoor,
                AllowedWeather = WeatherType.Warm,
                Season = Season.Summer,
                Location = "Provo"
            },

            new Activity
            {
                Name = "Family Movie Night",
                Description = "Watch a movie together at home.",
                Category = ActivityCategory.Entertainment,
                Cost = CostLevel.Low,
                MinParticipants = 2,
                MaxParticipants = 10,
                TimeOfDay = TimeOfDay.Evening,
                DurationMinutes = 150,
                Environment = ActivityEnvironment.Indoor,
                AllowedWeather = WeatherType.Any,
                Season = Season.Any,
                Location = "Home"
            },

            new Activity
            {
                Name = "Rock Climbing Gym",
                Description = "Indoor climbing and bouldering.",
                Category = ActivityCategory.Exercise,
                Cost = CostLevel.Medium,
                MinParticipants = 1,
                MaxParticipants = 8,
                TimeOfDay = TimeOfDay.Evening,
                DurationMinutes = 120,
                Environment = ActivityEnvironment.Indoor,
                AllowedWeather = WeatherType.Any,
                Season = Season.Any,
                Location = "Provo"
            },

            new Activity
            {
                Name = "Farmer's Market",
                Description = "Browse local produce and handmade goods.",
                Category = ActivityCategory.Social,
                Cost = CostLevel.Low,
                MinParticipants = 1,
                MaxParticipants = 10,
                TimeOfDay = TimeOfDay.Morning,
                DurationMinutes = 90,
                Environment = ActivityEnvironment.Outdoor,
                AllowedWeather = WeatherType.Warm,
                Season = Season.Summer,
                Location = "Downtown Provo"
            },

            new Activity
            {
                Name = "Escape Room Challenge",
                Description = "Solve puzzles with friends or family.",
                Category = ActivityCategory.Creative,
                Cost = CostLevel.High,
                MinParticipants = 2,
                MaxParticipants = 8,
                TimeOfDay = TimeOfDay.Evening,
                DurationMinutes = 90,
                Environment = ActivityEnvironment.Indoor,
                AllowedWeather = WeatherType.Any,
                Season = Season.Any,
                Location = "Orem"
            },

            new Activity
            {
                Name = "Campfire and S'mores",
                Description = "Evening campfire in the mountains.",
                Category = ActivityCategory.Relaxation,
                Cost = CostLevel.Low,
                MinParticipants = 2,
                MaxParticipants = 15,
                TimeOfDay = TimeOfDay.Evening,
                DurationMinutes = 180,
                Environment = ActivityEnvironment.Outdoor,
                AllowedWeather = WeatherType.Warm,
                Season = Season.Summer,
                Location = "Uintas"
            },

            new Activity
            {
                Name = "Library Reading Night",
                Description = "Attend a local library event or reading hour.",
                Category = ActivityCategory.Educational,
                Cost = CostLevel.Free,
                MinParticipants = 1,
                MaxParticipants = 20,
                TimeOfDay = TimeOfDay.Evening,
                DurationMinutes = 60,
                Environment = ActivityEnvironment.Indoor,
                AllowedWeather = WeatherType.Any,
                Season = Season.Any,
                Location = "Local Library"
            }

        );

        db.SaveChanges();
    }
}