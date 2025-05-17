using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using ModelsProvider;

namespace DataProvider;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;


    protected readonly IConfiguration Configuration;
    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // Move to AppSettings.json 
        options.UseSqlite(Configuration.GetConnectionString("Data Source=LocalDatabase.db"));
    }

}

