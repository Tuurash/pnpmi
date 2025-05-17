using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using ModelsProvider;

namespace DataProvider;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Team> Teams { get; set; } = null!;
    public DbSet<UserTask> Tasks { get; set; } = null!;


    protected readonly IConfiguration Configuration;
    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
    }


    //seed database with
    /* Admin: admin@demo.com / Admin123!
Manager: manager@demo.com / Manager123!
Employee: employee@demo.com / Employee123!
     */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(new User { Id = 1, FullName = "Admin", Email = "admin@demo.com", Password = "Admin123!", Role = UserRole.Admin });

        modelBuilder.Entity<User>().HasData(new User { Id = 2, FullName = "Manager", Email = "manager@demo.com", Password = "Manager123!", Role = UserRole.Manager });
        modelBuilder.Entity<User>().HasData(new User { Id = 3, FullName = "Employee", Email = "employee@demo.com", Password = "Employee123!", Role = UserRole.Employee });
    }

}

