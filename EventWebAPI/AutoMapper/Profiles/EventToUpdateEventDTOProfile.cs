using AutoMapper;
using EventWebAPI.Models.DTO.Event;
using EventWebAPI.Models.Entity;

namespace EventWebAPI.AutoMapper.Profiles
{
    public class EventToUpdateEventDTOProfile : Profile
    {
        public EventToUpdateEventDTOProfile()
        {
            CreateMap<Event, UpdateEventDTO>()
          .ForMember("SpeakerFullName", opt => opt.MapFrom(e => e.Speaker.Name));
        }
    }
}
