using EventWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventWebAPI.DataAccess
{
    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly AppDbContext context;

        public DataAccessProvider(DbContext context)
        {
            this.context = (AppDbContext)context;
        }

        public void AddSpeaker(Speaker speaker)
        {
            context.Speakers.Add(speaker);
            context.SaveChanges();
        }

        public void AddEvent(Event _event)
        {
            context.Entry(_event).State = EntityState.Added;
            context.SaveChanges();
        }

        public void UpdateSpeaker(Speaker speaker)
        {
            context.Speakers.Update(speaker);
            context.SaveChanges();
        }

        public void UpdateEvent(Event _event)
        {
            context.Events.Update(_event);
            context.SaveChanges();
        }

        public void DeleteEvent(int id)
        {
            var _event = context.Events.FirstOrDefault(t => t.Id == id);
            if (_event is not null)
            {
                context.Events.Remove(_event);
                context.SaveChanges();
            }            
        }

        public Event? GetEvent(int id)
        {
            var events = context.Events.Include(s => s.Speaker).ToList();

            if (events is not null)
            {
                var _event = events.FirstOrDefault(e => e.Id == id);

                if (_event is not null && _event.Speaker is not null)
                {
                    _event.Speaker.EventsId = events.Where(e => e.SpeakerId == _event.SpeakerId).Select(e => e.Id).ToList();
                }

                return _event;
            }

            return null;
        }

        public Speaker? GetSpeaker(int id)
        {
            var events = context.Events.Include(s => s.Speaker);

            if (events is not null)
            {
                var speaker = events.Select(e => e.Speaker).ToList().FirstOrDefault(s => s!=null && s.Id == id);

                if (speaker is not null)
                {
                    speaker.EventsId = events.Where(e => e.SpeakerId == speaker.Id).Select(e => e.Id).ToList();
                    return speaker;
                }

            }

            return null;
        }

        public List<Event> GetEvents()
        {
            var events = context.Events.Include(s => s.Speaker).ToList();

            foreach (var _event in events)
            {
                if (_event.Speaker is not null)
                {
                    _event.Speaker.EventsId = events.Where(e => e.SpeakerId == _event.SpeakerId).Select(e => e.Id).ToList();
                }                
            }

            return events;
        }

        public List<Speaker> GetSpeakers()
        {
            var events = context.Events.Include(s => s.Speaker);

            if (events is not null)
            {
                var speakers = events.Select(e => e.Speaker).ToList();

                if (speakers is not null)
                {
                    foreach (var speaker in speakers)
                    {
                        if (speaker is not null)
                        {
                            speaker.EventsId = events.Where(e => e.SpeakerId == speaker.Id).Select(e => e.Id).ToList();
                        }
                    }

                    return speakers;
                }               
            }

            return new List<Speaker>();
        }
    }
}
