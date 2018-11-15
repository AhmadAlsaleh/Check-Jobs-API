using Microsoft.EntityFrameworkCore;

namespace CheckJobsAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=CheckJobs;Trusted_Connection=True;");
            // optionsBuilder.UseSqlServer(@"Server=mi3-wsq3.a2hosting.com;Database=navarast_CheckJobs;User ID=navarast_Admin;password=P@ssw0rd;");

            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        #region Tables

        public DbSet<User> Users { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Spot> Spots { get; set; }
        public DbSet<Stared> Stareds { get; set; }
        public DbSet<Report> Reports { get; set; }
        
        #endregion
    }
}
