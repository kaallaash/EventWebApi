using EventWebAPI.Models.DTO.Event;
using EventWebAPI.Models.DTO.Speaker;

namespace EventWebAPI.DataAccess
{
    public interface IDataAccessProvider
    {
        Task AddSpeaker(CreateSpeakerDTO speaker);
        Task AddEvent(CreateEventDTO _event);
        Task<bool> UpdateSpeaker(SpeakerDTO speaker);
        Task<bool> UpdateEvent(UpdateEventDTO _event);
        Task<bool> DeleteEvent(int id);
        Task<SpeakerDetailsDTO?> GetSpeaker(int id);
        Task<EventDetailsDTO?> GetEvent(int id);
        Task<List<EventDetailsDTO>> GetEvents();
        Task<List<SpeakerDTO>> GetSpeakers();
    }
}
