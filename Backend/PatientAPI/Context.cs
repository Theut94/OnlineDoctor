using Microsoft.EntityFrameworkCore;
using ShareModels;

namespace PatientAPI
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        // Specify a default location for the SQLite database
        private const string DefaultDbPath = "Patients.db";

        // Define a DbSet for your Result entity
        public DbSet<Patient> Patients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // Always use the default location for the SQLite database
            options.UseSqlite($"Data Source={DefaultDbPath}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().HasKey(r => r.ssn);

            
        }
    }
}

