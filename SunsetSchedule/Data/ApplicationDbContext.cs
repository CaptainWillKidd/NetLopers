using Microsoft.EntityFrameworkCore;
using SunsetSchedule.Models;

namespace SunsetSchedule.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Activity> Activities { get; set; }

    public DbSet<ScheduledActivity> ScheduledActivities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ScheduledActivity>()
            .HasOne(sa => sa.Activity)
            .WithMany(a => a.ScheduledActivities)
            .HasForeignKey(sa => sa.ActivityId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}