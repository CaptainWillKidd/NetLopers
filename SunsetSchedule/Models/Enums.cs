namespace SunsetSchedule.Models;

public enum ActivityCategory
{
    Food,
    Exercise,
    Relaxation,
    Entertainment,
    Social,
    Outdoor,
    Creative,
    Educational
}

public enum CostLevel
{
    Free,
    Low,
    Medium,
    High
}

public enum TimeOfDay
{
    Anytime,
    Morning,
    Afternoon,
    Evening
}

public enum ActivityEnvironment
{
    Indoor,
    Outdoor,
    Either
}

public enum WeatherType
{
    Any,
    Sunny,
    Rainy,
    Snowy,
    Windy,
    Cold,
    Warm
}

public enum Season
{
    Any,
    Spring,
    Summer,
    Fall,
    Winter
}