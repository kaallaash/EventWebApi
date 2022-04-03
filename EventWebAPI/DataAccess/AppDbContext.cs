using EventWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventWebAPI.DataAccess
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration configuration;
        public DbSet<Event> Events { get; set; }
        public DbSet<Speaker> Speakers { get; set; }

        public AppDbContext(IConfiguration configuration) : base()
        {
            this.configuration = configuration;
            Database.EnsureCreated();

            if (this.Speakers != null && this.Speakers.Count() == 0)
            {
                var defaultSpeaker = new Speaker { Name = "Andrey" };
                Speakers.Add(defaultSpeaker);
                SaveChanges();
            }
            if (this.Events != null && this.Events.Count() == 0)
            {
                var defaultEvent = new Event
                {
                    Title = "Test",
                    Description = "Some description",
                    SpeakerId = 1,
                    Date = new DateTime(2022, 4, 1, 10, 0, 0)
                };
                this.Events.Add(defaultEvent);
                this.SaveChanges();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("PostgreSqlConnectionString"));
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            optionsBuilder.EnableSensitiveDataLogging(true);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }
}
