using AutoMapper;
using EventWebAPI.Models.DTO.Event;
using EventWebAPI.Models.Entity;

namespace EventWebAPI.AutoMapper.Profiles
{
    public class UpdateEventDTOToEventProfile : Profile
    {
        public UpdateEventDTOToEventProfile()
        {
            CreateMap<UpdateEventDTO, Event>();
        }
    }
}
