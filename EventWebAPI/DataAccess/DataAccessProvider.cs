using EventWebAPI.Models.Entity;
using EventWebAPI.Models.DTO.Event;
using EventWebAPI.Models.DTO.Speaker;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace EventWebAPI.DataAccess
{
    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly AppDbContext context;

        public DataAccessProvider(DbContext context)
        {
            this.context = (AppDbContext)context;
        }

        public EventDetailsModel? GetEvent(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Event, EventDetailsModel>()
           .ForMember("SpeakerName", opt => opt.MapFrom(ev => ev.Speaker.Name)));
            var mapper = new Mapper(config);
            var _event = mapper.Map<EventDetailsModel>(context.Events.Include(e => e.Speaker).FirstOrDefault(e => e.Id == id));
            return _event;
        }

        public SpeakerDetailsModel? GetSpeaker(int id)
        {
            if (id > 0)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Speaker, SpeakerDetailsModel>()
                    .ForMember("EventId", opt => opt.MapFrom(s => s.Events.Select(e => e.Id).ToList())));
                var mapper = new Mapper(config);
                return mapper.Map<SpeakerDetailsModel>(context.Speakers.Include(s => s.Events).FirstOrDefault(s => s.Id == id));
            }            

            return null;
        }

        public List<EventDetailsModel> GetEvents()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Event, EventDetailsModel>()
            .ForMember("SpeakerName", opt => opt.MapFrom(e => e.Speaker.Name)));
            var mapper = new Mapper(config);
            return mapper.Map<List<EventDetailsModel>>(context.Events.Include(s => s.Speaker));
        }

        public List<SpeakerDTO> GetSpeakers()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Speaker, SpeakerDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<List<SpeakerDTO>>(context.Speakers);
        }

        public void AddSpeaker(CreateSpeakerModel createSpeakerModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CreateSpeakerModel, Speaker>()
            .ForMember("Name", opt => opt.MapFrom(csm => csm.FirstName + " " + csm.LastName)));
            var mapper = new Mapper(config);
            var speaker = mapper.Map<Speaker>(createSpeakerModel);
            context.Speakers.Add(speaker);
            context.SaveChanges();
        }

        public void AddEvent(CreateEventModel createEventModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CreateEventModel, Event>());
            var mapper = new Mapper(config);
            var _event = mapper.Map<Event>(createEventModel);

            if (!context.Speakers.Any(s => s.Id == createEventModel.SpeakerId && s.Name == createEventModel.SpeakerName))
            {
                var speakerMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<CreateEventModel, Speaker>()
            .ForMember("Id", opt => opt.MapFrom(e => e.SpeakerId))
            .ForMember("Name", opt => opt.MapFrom(e => e.SpeakerName)));
                var speakerMapper = new Mapper(speakerMapperConfig);
                var speaker = speakerMapper.Map<Speaker>(createEventModel);
                context.Speakers.Add(speaker);
            }

            context.Events.Add(_event);
            context.SaveChanges();
        }

        public void UpdateSpeaker(SpeakerDTO speakerDTO)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<SpeakerDTO, Speaker>());
            var mapper = new Mapper(config);
            var speaker = mapper.Map<Speaker>(speakerDTO);
            context.Speakers.Update(speaker);
            context.SaveChanges();
        }

        public bool UpdateEvent(UpdateEventModel updateEventModel)
        {
            var eventById = context.Events.AsNoTracking().Include(e => e.Speaker).FirstOrDefault(e => e.Id == updateEventModel.Id);

            if (eventById is not null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Event, UpdateEventModel>()
          .ForMember("SpeakerName", opt => opt.MapFrom(e => e.Speaker.Name)));
                var mapper = new Mapper(config);
                var convertEvent = mapper.Map<UpdateEventModel>(eventById);

                if (convertEvent == updateEventModel
                    || (updateEventModel.SpeakerId != convertEvent.SpeakerId
                    && GetSpeaker(updateEventModel.SpeakerId) == null))
                {
                    return false;
                }

                var configFromUpdateToEvent = new MapperConfiguration(cfg => cfg.CreateMap<UpdateEventModel, Event>());
                var mapperFromUpdateToEvent = new Mapper(configFromUpdateToEvent);
                var _event = mapperFromUpdateToEvent.Map<Event>(updateEventModel);
                context.Events.Update(_event);
                context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool DeleteEvent(int id)
        {
            if (id > 0)
            {
                var _event = context.Events.FirstOrDefault(t => t.Id == id);

                if (_event is not null)
                {
                    context.Events.Remove(_event);
                    context.SaveChanges();
                    return true;
                }
            }

            return false;
        }
    }
}
