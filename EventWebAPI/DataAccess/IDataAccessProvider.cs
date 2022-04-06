using EventWebAPI.Models.Entity;
using EventWebAPI.Models.DTO.Event;
using EventWebAPI.Models.DTO.Speaker;

namespace EventWebAPI.DataAccess
{
    public interface IDataAccessProvider
    {
        Task AddSpeaker(CreateSpeakerModel speaker);
        Task AddEvent(CreateEventModel _event);
        Task<bool> UpdateSpeaker(SpeakerDTO speaker);
        Task<bool> UpdateEvent(UpdateEventModel _event);
        Task<bool> DeleteEvent(int id);
        Task<SpeakerDetailsModel?> GetSpeaker(int id);
        Task<EventDetailsModel?> GetEvent(int id);
        Task<List<EventDetailsModel>> GetEvents();
        Task<List<SpeakerDTO>> GetSpeakers();
    }
}
