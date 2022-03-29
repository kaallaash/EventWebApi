using EventWebAPI.Models;

namespace EventWebAPI.DataAccess
{
    public interface IDataAccessProvider
    {
        void AddSpeakerRecord(Speaker speaker);
        void AddEventRecord(Event _event);
        void UpdateSpeakerRecord(Speaker speaker);
        void UpdateEventRecord(Event _event);
        void DeleteEventRecord(int id);
        Speaker? GetSpeakerSingleRecord(int id);
        Event? GetEventSingleRecord(int id);
        List<Event> GetEventRecords();
        List<Speaker> GetSpeakerRecords();
    }
}
