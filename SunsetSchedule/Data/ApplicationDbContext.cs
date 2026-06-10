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

    public DbSet<ActivityImage> ActivityImages { get; set; }
    public DbSet<ScheduledActivity> ScheduledActivities { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ScheduledActivity>()
            .HasOne(sa => sa.Activity)
            .WithMany(a => a.ScheduledActivities)
            .HasForeignKey(sa => sa.ActivityId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ActivityImage>()
            .HasOne(ai => ai.Activity)
            .WithMany(a => a.Images)
            .HasForeignKey(ai => ai.ActivityId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}