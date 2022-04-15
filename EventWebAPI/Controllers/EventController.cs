using Microsoft.AspNetCore.Mvc;
using EventWebAPI.DataAccess;
using EventWebAPI.Models.DTO.Event;
using System.ComponentModel.DataAnnotations;

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
        [Route("All")]
        public async Task<IActionResult> GetEvents()
        {
            return Ok(await dataAccessProvider.GetEvents());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var _event = await dataAccessProvider.GetEvent(id);

            if (_event is null)
            {
                return BadRequest("This event does not exist.");
            }

            return Ok(_event);            
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateEventDTO _event)
        {
            if (_event is null)
            {
                return BadRequest("This event is not valid.");
            }

            await dataAccessProvider.AddEvent(_event);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit(UpdateEventDTO _event)
        {
            if (_event is null || !await dataAccessProvider.UpdateEvent(_event))
            {
                return BadRequest("This event is not valid.");
            }

            return Ok();
        }
       
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await dataAccessProvider.DeleteEvent(id))
            {
                return BadRequest("This event does not exist.");
            }

            return Ok();          
        }
    }
}
