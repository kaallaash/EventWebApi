using EventWebAPI.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace EventWebAPI.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Speaker> Speakers { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Speaker>().HasData(new Speaker {Id = 1, Name = "Andrey"});
            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    Id = 1,
                    Title = "Test",
                    Description = "Some description",
                    SpeakerId = 1,
                    Date = new DateTime(2022, 4, 1, 10, 0, 0)
                });
        }
    }
}