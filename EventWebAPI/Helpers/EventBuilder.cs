using EventWebAPI.DataAccess;
using EventWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventWebAPI.Helpers
{
    public class EventBuilder : IBuider<Event?>
    {
        private EventsContext? Db { get; set; }
        private int EventId { get; set; } 
        public EventBuilder(int eventId)
        {
            EventId = eventId;
        }

        public async Task<Event?> BuildAsync()
        {
            using (Db = new EventsContext())
            {
                var _event = await Db.Events.FirstOrDefaultAsync(e => e.Id == EventId);

                if (_event is not null)
                {
                    _event.Speaker = await new SpeakerBuilder(_event.SpeakerId).BuildAsync();
                }

                return _event;
            }            
        }

        public Event? Build()
        {
            using (Db = new EventsContext())
            {
                var _event = Db.Events.FirstOrDefault(e => e.Id == EventId);

                if (_event is not null)
                {
                    _event.Speaker = new SpeakerBuilder(_event.SpeakerId).Build();
                }

                return _event;
            }
        }
    }
}
