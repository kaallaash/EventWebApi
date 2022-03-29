using EventWebAPI.DataAccess;
using EventWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventWebAPI.Helpers
{
    public class SpeakerBuilder : IBuider<Speaker?>
    {
        private EventsContext? Db { get; set; }
        private int SpeakerId { get; set; }
        public SpeakerBuilder(int speakerId)
        {
            SpeakerId = speakerId;                
        }

        public async Task<Speaker?> BuildAsync()
        {
            using (Db = new EventsContext())
            {
                var speaker = await Db.Speakers.FirstOrDefaultAsync(s => s.Id == SpeakerId);

                if (speaker is not null)
                {
                    var events = Db.Events.Where(e => e.SpeakerId == speaker.Id).ToList();

                    if (events is not null)
                    {
                        speaker.EventsId = new List<int>();

                        foreach (var e in events)
                        {
                            speaker.EventsId.Add(e.Id);
                        }
                    }
                }

                return speaker;
            }
        }

        public Speaker? Build()
        {
            using (Db = new EventsContext())
            {
                var speaker = Db.Speakers.FirstOrDefault(s => s.Id == SpeakerId);

                if (speaker is not null)
                {
                    var events = Db.Events.Where(e => e.SpeakerId == speaker.Id).ToList();

                    if (events is not null)
                    {
                        speaker.EventsId = new List<int>();

                        foreach (var e in events)
                        {
                            speaker.EventsId.Add(e.Id);
                        }
                    }
                }

                return speaker;
            }            
        }
    }
}