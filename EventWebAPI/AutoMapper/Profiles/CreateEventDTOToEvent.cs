using AutoMapper;
using EventWebAPI.Models.DTO.Event;
using EventWebAPI.Models.Entity;

namespace EventWebAPI.AutoMapper.Profiles
{
    public class CreateEventDTOToEvent : Profile
    {
        public CreateEventDTOToEvent()
        {
            CreateMap<CreateEventDTO, Event>();
        }
    }
}
