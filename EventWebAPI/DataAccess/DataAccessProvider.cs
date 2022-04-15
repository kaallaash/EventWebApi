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
        private readonly IMapper mapper;

        public DataAccessProvider(DbContext context, IMapper mapper)
        {
            this.context = (AppDbContext)context;
            this.mapper = mapper;
        }

        public async Task<EventDetailsDTO?> GetEvent(int id)
        {
            if (id > 0)
            {
                var _event = mapper.Map<EventDetailsDTO>
                    (await context.Events.Include(e => e.Speaker).FirstOrDefaultAsync(e => e.Id == id));
                return _event;
            }

            return null;            
        }

        public async Task<SpeakerDetailsDTO?> GetSpeaker(int id)
        {
            if (id > 0)
            {
                return mapper.Map<SpeakerDetailsDTO>
                    (await context.Speakers.Include(s => s.Events).FirstOrDefaultAsync(s => s.Id == id));
            }            

            return null;
        }

        public async Task <List<EventDetailsDTO>> GetEvents()
        {
            return mapper.Map<List<EventDetailsDTO>>(await context.Events.Include(s => s.Speaker).OrderBy(e => e.Id).ToListAsync());
        }

        public async Task<List<SpeakerDTO>> GetSpeakers()
        {
            return mapper.Map<List<SpeakerDTO>>(await context.Speakers.OrderBy(s => s.Id).ToListAsync());
        }

        public async Task AddSpeaker(CreateSpeakerDTO createSpeakerModel)
        {
            var speaker = mapper.Map<Speaker>(createSpeakerModel);
            await context.Speakers.AddAsync(speaker);
            await context.SaveChangesAsync();
        }

        public async Task AddEvent(CreateEventDTO createEventModel)
        {
            var _event = mapper.Map<Event>(createEventModel);

            if (!await context.Speakers.AnyAsync(s => s.Id == createEventModel.SpeakerId && s.Name == createEventModel.SpeakerFullName))
            {
                var speaker = mapper.Map<Speaker>(createEventModel);
                await context.Speakers.AddAsync(speaker);
            }

            await context.Events.AddAsync(_event);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UpdateSpeaker(SpeakerDTO speakerDTO)
        {
            var speakerById = await context.Speakers.FirstOrDefaultAsync(s => s.Id == speakerDTO.Id);

            if (speakerById is not null)
            {
                var speaker = mapper.Map<Speaker>(speakerDTO);
                speakerById.Name = speaker.Name;
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateEvent(UpdateEventDTO updateEventModel)
        {
            var eventById = await context.Events.Include(e => e.Speaker)
               .FirstOrDefaultAsync(e => e.Id == updateEventModel.Id);

            if (eventById is not null)
            {
                var convertEvent = mapper.Map<UpdateEventDTO>(eventById);

                if (convertEvent is null || updateEventModel.Equals(convertEvent))
                {
                    return false;
                }

                if (updateEventModel.SpeakerId != convertEvent.SpeakerId)
                {
                    var speaker = await GetSpeaker(updateEventModel.SpeakerId);

                    if (speaker != null && speaker.Name != updateEventModel.SpeakerFullName)
                    {
                        return false;
                    }
                }

                var _event = mapper.Map<Event>(updateEventModel);

                eventById.SpeakerId = _event.SpeakerId;
                eventById.Title = _event.Title;
                eventById.Description = _event.Description;
                eventById.Date = _event.Date;
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
