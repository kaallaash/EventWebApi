using AutoMapper;
using EventWebAPI.Models.DTO.Event;
using EventWebAPI.Models.Entity;

namespace EventWebAPI.AutoMapper.Profiles
{
    public class CreateEventDTOToSpeakerProfile : Profile
    {
        public CreateEventDTOToSpeakerProfile()
        {
            CreateMap<CreateEventDTO, Speaker>()
            .ForMember("Id", opt => opt.MapFrom(e => e.SpeakerId))
            .ForMember("Name", opt => opt.MapFrom(e => e.SpeakerFullName));
        }
    }
}
