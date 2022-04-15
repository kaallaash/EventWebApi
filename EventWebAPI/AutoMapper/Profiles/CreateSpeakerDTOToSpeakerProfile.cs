using AutoMapper;
using EventWebAPI.Models.DTO.Speaker;
using EventWebAPI.Models.Entity;

namespace EventWebAPI.AutoMapper.Profiles
{
    public class CreateSpeakerDTOToSpeakerProfile : Profile
    {
        public CreateSpeakerDTOToSpeakerProfile()
        {
            CreateMap<CreateSpeakerDTO, Speaker>()
             .ForMember("Name", opt => opt.MapFrom(csm => csm.FirstName + " " + csm.LastName));
        }
    }
}
