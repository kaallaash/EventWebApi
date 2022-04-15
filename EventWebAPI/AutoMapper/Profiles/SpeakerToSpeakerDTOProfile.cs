using AutoMapper;
using EventWebAPI.Models.DTO.Speaker;
using EventWebAPI.Models.Entity;

namespace EventWebAPI.AutoMapper.Profiles
{
    public class SpeakerToSpeakerDTOProfile : Profile
    {
        public SpeakerToSpeakerDTOProfile()
        {
            CreateMap<Speaker, SpeakerDTO>();
        }
    }
}
