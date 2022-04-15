using AutoMapper;
using EventWebAPI.AutoMapper.Profiles;

namespace EventWebAPI.AutoMapper
{
    public class MappingProfile
    {
        public static MapperConfiguration InitializeAutoMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CreateEventDTOToEvent());
                cfg.AddProfile(new CreateEventDTOToSpeakerProfile());
                cfg.AddProfile(new CreateSpeakerDTOToSpeakerProfile());
                cfg.AddProfile(new EventToEventDetailsDTOProfile());
                cfg.AddProfile(new EventToUpdateEventDTOProfile());
                cfg.AddProfile(new SpeakerDTOToSpeakerProfile());
                cfg.AddProfile(new SpeakerToSpeakerDetailsDTOProfile());
                cfg.AddProfile(new SpeakerToSpeakerDTOProfile());
                cfg.AddProfile(new UpdateEventDTOToEventProfile());
            });

            return config;
        }
    }
}
