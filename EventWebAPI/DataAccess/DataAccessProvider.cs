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

        public async Task<EventDetailsModel?> GetEvent(int id)
        {
            if (id > 0)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Event, EventDetailsModel>()
                           .ForMember("SpeakerName", opt => opt.MapFrom(ev => ev.Speaker.Name)));
                var mapper = new Mapper(config);
                var _event = mapper.Map<EventDetailsModel>
                    (await context.Events.Include(e => e.Speaker).FirstOrDefaultAsync(e => e.Id == id));
                return _event;
            }

            return null;            
        }

        public async Task<SpeakerDetailsModel?> GetSpeaker(int id)
        {
            if (id > 0)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Speaker, SpeakerDetailsModel>()
                    .ForMember("EventId", opt => opt.MapFrom(s => s.Events.Select(e => e.Id).ToList())));
                var mapper = new Mapper(config);
                return mapper.Map<SpeakerDetailsModel>
                    (await context.Speakers.Include(s => s.Events).FirstOrDefaultAsync(s => s.Id == id));
            }            

            return null;
        }

        public async Task <List<EventDetailsModel>> GetEvents()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Event, EventDetailsModel>()
            .ForMember("SpeakerName", opt => opt.MapFrom(e => e.Speaker.Name)));
            var mapper = new Mapper(config);
            return mapper.Map<List<EventDetailsModel>>(await context.Events.Include(s => s.Speaker).ToListAsync());
        }

        public async Task<List<SpeakerDTO>> GetSpeakers()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Speaker, SpeakerDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<List<SpeakerDTO>>(await context.Speakers.ToListAsync());
        }

        public async Task AddSpeaker(CreateSpeakerModel createSpeakerModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CreateSpeakerModel, Speaker>()
            .ForMember("Name", opt => opt.MapFrom(csm => csm.FirstName + " " + csm.LastName)));
            var mapper = new Mapper(config);
            var speaker = mapper.Map<Speaker>(createSpeakerModel);
            await context.Speakers.AddAsync(speaker);
            await context.SaveChangesAsync();
        }

        public async Task AddEvent(CreateEventModel createEventModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CreateEventModel, Event>());
            var mapper = new Mapper(config);
            var _event = mapper.Map<Event>(createEventModel);

            if (!await context.Speakers.AnyAsync(s => s.Id == createEventModel.SpeakerId && s.Name == createEventModel.SpeakerName))
            {
                var speakerMapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<CreateEventModel, Speaker>()
            .ForMember("Id", opt => opt.MapFrom(e => e.SpeakerId))
            .ForMember("Name", opt => opt.MapFrom(e => e.SpeakerName)));
                var speakerMapper = new Mapper(speakerMapperConfig);
                var speaker = speakerMapper.Map<Speaker>(createEventModel);
                await context.Speakers.AddAsync(speaker);
            }

            await context.Events.AddAsync(_event);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateSpeaker(SpeakerDTO speakerDTO)
        {
            var speakerById = await context.Speakers.AsNoTracking().FirstOrDefaultAsync(s => s.Id == speakerDTO.Id);

            if (speakerById is not null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<SpeakerDTO, Speaker>());
                var mapper = new Mapper(config);
                var speaker = mapper.Map<Speaker>(speakerDTO);
                context.Speakers.Update(speaker);
                await context.SaveChangesAsync();
                return true;
            }

            return false;           
        }

        public async Task<bool> UpdateEvent(UpdateEventModel updateEventModel)
        {
            var eventById = await context.Events.AsNoTracking().Include(e => e.Speaker)
                .FirstOrDefaultAsync(e => e.Id == updateEventModel.Id);

            if (eventById is not null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Event, UpdateEventModel>()
          .ForMember("SpeakerName", opt => opt.MapFrom(e => e.Speaker.Name)));
                var mapper = new Mapper(config);
                var convertEvent = mapper.Map<UpdateEventModel>(eventById);

                if (convertEvent == updateEventModel
                    || (updateEventModel.SpeakerId != convertEvent.SpeakerId
                    && await GetSpeaker(updateEventModel.SpeakerId) == null))
                {
                    return false;
                }

                var configFromUpdateToEvent = new MapperConfiguration(cfg => cfg.CreateMap<UpdateEventModel, Event>());
                var mapperFromUpdateToEvent = new Mapper(configFromUpdateToEvent);
                var _event = mapperFromUpdateToEvent.Map<Event>(updateEventModel);
                context.Events.Update(_event);
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteEvent(int id)
        {
            if (id > 0)
            {
                var _event = await context.Events.FirstOrDefaultAsync(t => t.Id == id);

                if (_event is not null)
                {
                    context.Events.Remove(_event);
                    await context.SaveChangesAsync();
                    return true;
                }
            }

            return false;
        }
    }
}
