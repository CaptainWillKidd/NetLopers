namespace SunsetSchedule.Models;

public class FilterModel
{
    public ActivityEnvironment? Environment {get; set;}
    public CostLevel? Cost {get; set;}
    public WeatherType? Weather {get; set;}
    public TimeOfDay? TimeOfDay {get; set;}
    public ActivityCategory? Category {get; set;}
    public Season? Season {get; set;}
    public int GroupSize {get; set;}
}