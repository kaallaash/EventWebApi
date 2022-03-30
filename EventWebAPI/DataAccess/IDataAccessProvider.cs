using EventWebAPI.Models;

namespace EventWebAPI.DataAccess
{
    public interface IDataAccessProvider
    {
        void AddSpeaker(Speaker speaker);
        void AddEvent(Event _event);
        void UpdateSpeaker(Speaker speaker);
        void UpdateEvent(Event _event);
        void DeleteEvent(int id);
        Speaker? GetSpeaker(int id);
        Event? GetEvent(int id);
        List<Event> GetEvents();
        List<Speaker> GetSpeakers();
    }
}
