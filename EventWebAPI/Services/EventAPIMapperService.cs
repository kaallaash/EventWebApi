using AutoMapper;
using EventWebAPI.Models.Entity;
using EventWebAPI.Models.DTO.Event;
using EventWebAPI.Models.DTO.Speaker;

namespace EventWebAPI.Services
{
    public class EventAPIMapperService : IEventAPIMapperService
    {
        public Mapper GetEventToEventDetailsDTOMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Event, EventDetailsDTO>()
                          .ForMember("SpeakerName", opt => opt.MapFrom(ev => ev.Speaker.Name)));
            return new Mapper(config);
        }

        public Mapper GetSpeakerToSpeakerDetailsDTOMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Speaker, SpeakerDetailsDTO>()
                    .ForMember("EventId", opt => opt.MapFrom(s => s.Events.Select(e => e.Id).ToList())));
            return new Mapper(config);
        }

        public Mapper GetSpeakerToSpeakerDTOMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Speaker, SpeakerDTO>());
            return new Mapper(config);
        }

        public Mapper GetCreateSpeakerDTOToSpeakerMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CreateSpeakerDTO, Speaker>()
             .ForMember("Name", opt => opt.MapFrom(csm => csm.FirstName + " " + csm.LastName)));
            return new Mapper(config);
        }

        public Mapper GetCreateEventDTOToEventMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CreateEventDTO, Event>());
            return new Mapper(config);
        }

        public Mapper GetCreateEventDTOToSpeakerMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CreateEventDTO, Speaker>()
            .ForMember("Id", opt => opt.MapFrom(e => e.SpeakerId))
            .ForMember("Name", opt => opt.MapFrom(e => e.SpeakerName)));
            return new Mapper(config);
        }

        public Mapper GetSpeakerDTOToSpeakerMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<SpeakerDTO, Speaker>());
            return new Mapper(config);
        }

        public Mapper GetEventToUpdateEventDTOMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Event, UpdateEventDTO>()
          .ForMember("SpeakerName", opt => opt.MapFrom(e => e.Speaker.Name)));
            return new Mapper(config);
        }

        public Mapper GetUpdateEventDTOToEventMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateEventDTO, Event>());
            return new Mapper(config);
        }
    }
}
