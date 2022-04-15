using AutoMapper;
using EventWebAPI.Models.DTO.Speaker;
using EventWebAPI.Models.Entity;

namespace EventWebAPI.AutoMapper.Profiles
{
    public class SpeakerDTOToSpeakerProfile : Profile
    {
        public SpeakerDTOToSpeakerProfile()
        {
            CreateMap<SpeakerDTO, Speaker>();
        }
    }
}
