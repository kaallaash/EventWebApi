using AutoMapper;
using EventWebAPI.Models.DTO.Speaker;
using EventWebAPI.Models.Entity;

namespace EventWebAPI.AutoMapper.Profiles
{
    public class SpeakerToSpeakerDetailsDTOProfile : Profile
    {
        public SpeakerToSpeakerDetailsDTOProfile()
        {
            CreateMap<Speaker, SpeakerDetailsDTO>()
                    .ForMember("EventId", opt => opt.MapFrom(s => s.Events.Select(e => e.Id).ToList()));
        }
    }
}
