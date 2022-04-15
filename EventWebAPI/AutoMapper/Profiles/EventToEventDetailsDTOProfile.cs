using AutoMapper;
using EventWebAPI.Models.DTO.Event;
using EventWebAPI.Models.Entity;

namespace EventWebAPI.AutoMapper.Profiles
{
    public class EventToEventDetailsDTOProfile : Profile
    {
        public EventToEventDetailsDTOProfile()
        {
            CreateMap<Event, EventDetailsDTO>()
                .ForMember("SpeakerName", opt => opt.MapFrom(ev => ev.Speaker.Name));
        }
    }
}
