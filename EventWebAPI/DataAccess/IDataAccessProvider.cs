using EventWebAPI.Models.Entity;
using EventWebAPI.Models.DTO.Event;
using EventWebAPI.Models.DTO.Speaker;

namespace EventWebAPI.DataAccess
{
    public interface IDataAccessProvider
    {
        void AddSpeaker(CreateSpeakerModel speaker);
        void AddEvent(CreateEventModel _event);
        void UpdateSpeaker(SpeakerDTO speaker);
        bool UpdateEvent(UpdateEventModel _event);
        bool DeleteEvent(int id);
        SpeakerDetailsModel? GetSpeaker(int id);
        EventDetailsModel? GetEvent(int id);
        List<EventDetailsModel> GetEvents();
        List<SpeakerDTO> GetSpeakers();
    }
}
