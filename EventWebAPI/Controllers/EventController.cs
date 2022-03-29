using EventWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using EventWebAPI.DataAccess;
using EventWebAPI.Helpers;

namespace EventWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public EventController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetEvents()
        {
            var events = _dataAccessProvider.GetEventRecords();

            if (events is not null)
            {
                for (int i = 0; i < events.Count; i++)
                {
                    events[i].Speaker = _dataAccessProvider.GetSpeakerSingleRecord(events[i].SpeakerId);//модернизировать на async                    
                }
            }

            return Ok(events);
        }

        [HttpGet]
        [Route("Speakers")]
        public async Task<IActionResult> GetSpeakers()
        {
            var speakers = _dataAccessProvider.GetSpeakerRecords();//модернизировать на async  
            if (speakers is not null)
            {
                for (int i = 0; i < speakers.Count; i++)
                {
                    var speaker = _dataAccessProvider.GetSpeakerSingleRecord(speakers[i].Id);//модернизировать на async 
                    if (speaker is not null)
                    {
                        speakers[i] = speaker;
                    }
                }
            }
            return Ok(speakers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            if (id > 0)
            {
                var _event = _dataAccessProvider.GetEventSingleRecord(id);//модернизировать на async 
                if (_event is null)
                {
                    return NotFound("This event does not exist.");
                }
                return Ok(_event);
            }
            return NotFound("Id isn't correct.");
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Event _event)
        {
            if (_event is null)
            {
                return NotFound("Event is null");
            }

            var eventById = _dataAccessProvider.GetEventSingleRecord(_event.Id);//модернизировать на async

            if (eventById is not null)
            {
                return NotFound("An event with this ID already exists.");
            }

            if (_event.Speaker is null)
            {
                return NotFound("Provide a speaker.");
            }

            var speakerById = _dataAccessProvider.GetSpeakerSingleRecord(_event.SpeakerId);//модернизировать на async

            if (speakerById is null)
            {
                _dataAccessProvider.AddSpeakerRecord(_event.Speaker);
            }
            else if (speakerById.Name != _event.Speaker.Name)
            {
                return NotFound("Non - correct speaker.");
            }

            _dataAccessProvider.AddEventRecord(_event);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit(Event _event)
        {
            if (_event is null)
            {
                return NotFound("Event is null");
            }

            var eventById = _dataAccessProvider.GetEventSingleRecord(_event.Id);//модернизировать на async

            if (eventById is null)
            {
                return NotFound("This event was not found");
            }

            var speakerById = _dataAccessProvider.GetSpeakerSingleRecord(_event.SpeakerId);//модернизировать на async

            if (speakerById == null && _event.Speaker != null)
            {
                _dataAccessProvider.AddSpeakerRecord(_event.Speaker);
            }
            else if (speakerById is null && _event.Speaker is null)
            {
                return NotFound("You need provide an existing or correct speaker.");
            }
            else if (speakerById is not null
                && _event.Speaker is not null
                && speakerById.Id != _event.Speaker.Id
                && speakerById.Name != _event.Speaker.Name)
            {
                _dataAccessProvider.UpdateSpeakerRecord(_event.Speaker);
            }

            eventById.Title = _event.Title;
            eventById.Description = _event.Description;
            eventById.SpeakerId = _event.SpeakerId;
            _dataAccessProvider.UpdateEventRecord(eventById);
            return Ok();
        }
       
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id > 0)
            {
                var eventById = _dataAccessProvider.GetEventSingleRecord(id);//модернизировать на async

                if (eventById is not null)
                {
                    _dataAccessProvider.DeleteEventRecord(id);
                    return Ok();
                }
                else
                {
                    return NotFound("This Event doesn't exist.");
                }
            }
            return NotFound("Id isn't correct.");
        }
    }
}
