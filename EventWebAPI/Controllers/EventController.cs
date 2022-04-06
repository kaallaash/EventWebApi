using EventWebAPI.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using EventWebAPI.DataAccess;
using EventWebAPI.Models.DTO.Event;
using EventWebAPI.Models.DTO.Speaker;

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
            return Ok(dataAccessProvider.GetEvents());
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            if (id > 0)
            {
                var _event = dataAccessProvider.GetEvent(id);

                if (_event is not null)
                {
                    return Ok(_event);
                }
            }

            return BadRequest("This event does not exist.");
        }

        [HttpPost]
        public IActionResult Add(CreateEventModel _event)
        {
            if (_event is null)
            {
                return BadRequest("Event is null");
            }

            dataAccessProvider.AddEvent(_event);
            return Ok();
        }

        [HttpPut]
        public IActionResult Edit(UpdateEventModel _event)
        {
            if (_event is null)
            {
                return BadRequest("Event is null");
            }

            if (dataAccessProvider.UpdateEvent(_event))
            {
                return Ok();
            }

            return BadRequest("This event is not valid.");
        
        }
       
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (dataAccessProvider.DeleteEvent(id))
            {                
                return Ok();
            }

            return BadRequest("This event does not exist.");
        }
    }
}
