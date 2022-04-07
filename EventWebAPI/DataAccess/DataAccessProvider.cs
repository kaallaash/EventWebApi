using EventWebAPI.Models.Entity;
using EventWebAPI.Models.DTO.Event;
using EventWebAPI.Models.DTO.Speaker;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using EventWebAPI.Services;

namespace EventWebAPI.DataAccess
{
    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly AppDbContext context;
        private readonly IEventAPIMapperService mapper;

        public DataAccessProvider(DbContext context, IEventAPIMapperService mapper)
        {
            this.context = (AppDbContext)context;
            this.mapper = mapper;
        }

        public async Task<EventDetailsModel?> GetEvent(int id)
        {
            if (id > 0)
            {
                var mapper = this.mapper.GetEventToEventDetailsModelMapper();
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
                var mapper = this.mapper.GetSpeakerToSpeakerDetailsModelMapper();
                return mapper.Map<SpeakerDetailsModel>
                    (await context.Speakers.Include(s => s.Events).FirstOrDefaultAsync(s => s.Id == id));
            }            

            return null;
        }

        public async Task <List<EventDetailsModel>> GetEvents()
        {
            var mapper = this.mapper.GetEventToEventDetailsModelMapper();
            return mapper.Map<List<EventDetailsModel>>(await context.Events.Include(s => s.Speaker).ToListAsync());
        }

        public async Task<List<SpeakerDTO>> GetSpeakers()
        {
            var mapper = this.mapper.GetSpeakerToSpeakerDTOMapper();
            return mapper.Map<List<SpeakerDTO>>(await context.Speakers.ToListAsync());
        }

        public async Task AddSpeaker(CreateSpeakerModel createSpeakerModel)
        {
            var mapper = this.mapper.GetCreateSpeakerModelToSpeakerMapper();
            var speaker = mapper.Map<Speaker>(createSpeakerModel);
            await context.Speakers.AddAsync(speaker);
            await context.SaveChangesAsync();
        }

        public async Task AddEvent(CreateEventModel createEventModel)
        {
            var eventMapper = mapper.GetCreateEventModelToEventMapper();
            var _event = eventMapper.Map<Event>(createEventModel);

            if (!await context.Speakers.AnyAsync(s => s.Id == createEventModel.SpeakerId && s.Name == createEventModel.SpeakerName))
            {
                var speakerMapper = mapper.GetCreateEventModelToSpeakerMapper();
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
                var mapper = this.mapper.GetSpeakerDTOToSpeakerMapper();
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
                var mapper = this.mapper.GetEventToUpdateEventModelMapper();
                var convertEvent = mapper.Map<UpdateEventModel>(eventById);

                if (convertEvent == updateEventModel
                    || (updateEventModel.SpeakerId != convertEvent.SpeakerId
                    && await GetSpeaker(updateEventModel.SpeakerId) == null))
                {
                    return false;
                }

                mapper = this.mapper.GetEventToUpdateEventModelMapper();
                var _event = mapper.Map<Event>(updateEventModel);
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
