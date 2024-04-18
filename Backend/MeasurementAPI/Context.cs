using Microsoft.EntityFrameworkCore;
using ShareModels;

namespace MeasurementAPI;
public class Context : DbContext
{
    public Context() { }
    
    public Context(DbContextOptions<Context> options) : base(options) {
    }

    // Specify a default location for the SQLite database
    private const string DefaultDbPath = "Meassurement.db";

    // Define a DbSet for your Result entity
    public DbSet<Measurement> Measurements { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // Always use the default location for the SQLite database
        options.UseSqlite($"Data Source={DefaultDbPath}");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Measurement>().HasKey(r => r.Id);
        
        // Auto increment the Id field
        modelBuilder.Entity<Measurement>()
            .Property(r => r.Id)
            .ValueGeneratedOnAdd();
    }

}
