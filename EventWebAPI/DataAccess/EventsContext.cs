using EventWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventWebAPI.DataAccess
{
    public class EventsContext : DbContext
    {
        public EventsContext():base()
        {
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
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=eventsdb;Username=postgres;Password=278202;");
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            optionsBuilder.EnableSensitiveDataLogging(true);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
       
    }
}
