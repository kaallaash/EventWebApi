using EventWebAPI.Models;

namespace EventWebAPI.DataAccess
{
    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly EventsContext _context = new EventsContext();

        public void AddSpeakerRecord(Speaker speaker)
        {
            _context.Speakers.Add(speaker);
            _context.SaveChanges();
        }

        public void AddEventRecord(Event _event)
        {
            _context.Events.Add(_event);
            _context.SaveChanges();
        }

        public void UpdateSpeakerRecord(Speaker speaker)
        {
            _context.Speakers.Update(speaker);
            _context.SaveChanges();
        }

        public void UpdateEventRecord(Event _event)
        {
            _context.Events.Update(_event);
            _context.SaveChanges();
        }

        public void DeleteEventRecord(int id)
        {
            var _event = _context.Events.FirstOrDefault(t => t.Id == id);
            if (_event is not null)
            {
                _context.Events.Remove(_event);
                _context.SaveChanges();
            }            
        }

        public Event? GetEventSingleRecord(int id)
        {
            var _event = _context.Events.FirstOrDefault(e => e.Id == id);

            if (_event is not null)
            {
                _event.Speaker = GetSpeakerSingleRecord(_event.SpeakerId);
            }

            return _event;
        }

        public Speaker? GetSpeakerSingleRecord(int id)
        {
            var speaker = _context.Speakers.FirstOrDefault(s => s.Id == id);

            if (speaker is not null)
            {
                var events = _context.Events.Where(e => e.SpeakerId == speaker.Id).ToList();

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

        public List<Event> GetEventRecords()
        {
            return _context.Events.ToList();
        }

        public List<Speaker> GetSpeakerRecords()
        {
            return _context.Speakers.ToList();
        }
    }
}
