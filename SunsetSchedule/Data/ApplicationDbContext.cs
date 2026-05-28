using Microsoft.EntityFrameworkCore;
using SunsetSchedule.Models;

namespace SunsetSchedule.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Activity> Activities => Set<Activity>();
}