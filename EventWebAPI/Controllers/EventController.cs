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
        public async Task<IActionResult> GetEvents()
        {
            return Ok(await dataAccessProvider.GetEvents());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var _event = await dataAccessProvider.GetEvent(id);

            if (_event is not null)
            {
                return Ok(_event);
            }

            return BadRequest("This event does not exist.");
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateEventModel _event)
        {
            if (_event is null)
            {
                return BadRequest("Event is null");
            }

            await dataAccessProvider.AddEvent(_event);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit(UpdateEventModel _event)
        {
            if (_event is null)
            {
                return BadRequest("Event is null");
            }

            if (await dataAccessProvider.UpdateEvent(_event))
            {
                return Ok();
            }

            return BadRequest("This event is not valid.");
        
        }
       
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (await dataAccessProvider.DeleteEvent(id))
            {                
                return Ok();
            }

            return BadRequest("This event does not exist.");
        }
    }
}
