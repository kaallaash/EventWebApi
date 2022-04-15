using EventWebAPI.DataAccess;
using EventWebAPI.Models.DTO.Speaker;
using Microsoft.AspNetCore.Mvc;

namespace EventWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SpeakerController : ControllerBase
    {
        private readonly IDataAccessProvider dataAccessProvider;

        public SpeakerController(IDataAccessProvider dataAccessProvider)
        {
            this.dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetSpeakers()
        {
            return Ok(await dataAccessProvider.GetSpeakers());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var speaker = await dataAccessProvider.GetSpeaker(id);

            if (speaker is null)
            {
                return NotFound("This speaker does not found.");
            }

            return Ok(speaker);            
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateSpeakerDTO speaker)
        {
            await dataAccessProvider.AddSpeaker(speaker);
            return Ok();
        }
    }
}
