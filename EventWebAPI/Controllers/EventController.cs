using EventWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using EventWebAPI.DataAccess;

namespace EventWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IDataAccessProvider dataAccessProvider;

        public EventController(IDataAccessProvider dataAccessProvider)
        {
            this.dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetEvents()
        {
            return Ok(dataAccessProvider.GetEvents().OrderBy(e => e.Id));
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            if (id > 0)
            {
                var _event = dataAccessProvider.GetEvent(id);

                if (_event is null)
                {
                    return NotFound("This event does not exist.");
                }

                return Ok(_event);
            }

            return NotFound("Id isn't correct.");
        }

        [HttpPost]
        public IActionResult Add(Event _event)
        {
            if (_event is null)
            {
                return NotFound("Event is null");
            }

            var eventById = dataAccessProvider.GetEvent(_event.Id);

            if (eventById is not null)
            {
                return NotFound("An event with this ID already exists.");
            }

            if (_event.Speaker is null)
            {
                return NotFound("Provide a speaker.");
            }

            var speakerById = dataAccessProvider.GetSpeaker(_event.SpeakerId);

            if (speakerById is null)
            {
                dataAccessProvider.AddSpeaker(_event.Speaker);
            }
            else if (speakerById.Name != _event.Speaker.Name)
            {
                return NotFound("Non - correct speaker.");
            }

            dataAccessProvider.AddEvent(_event);
            return Ok();
        }

        [HttpPut]
        public IActionResult Edit(Event _event)
        {
            if (_event is null)
            {
                return NotFound("Event is null");
            }

            var eventById = dataAccessProvider.GetEvent(_event.Id);

            if (eventById is null)
            {
                return NotFound("This event was not found");
            }

            var speakerById = dataAccessProvider.GetSpeaker(_event.SpeakerId);

            if (speakerById == null && _event.Speaker != null)
            {
                dataAccessProvider.AddSpeaker(_event.Speaker);
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
                dataAccessProvider.UpdateSpeaker(_event.Speaker);
            }

            eventById.Title = _event.Title;
            eventById.Description = _event.Description;
            eventById.SpeakerId = _event.SpeakerId;
            dataAccessProvider.UpdateEvent(eventById);
            return Ok();
        }
       
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                var eventById = dataAccessProvider.GetEvent(id);

                if (eventById is not null)
                {
                    dataAccessProvider.DeleteEvent(id);
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
