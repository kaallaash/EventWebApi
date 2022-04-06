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
        [Route("GetAll")]
        public async Task<IActionResult> GetSpeakers()
        {
            return Ok(await dataAccessProvider.GetSpeakers());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var speaker = await dataAccessProvider.GetSpeaker(id);

            if (speaker is not null)
            {
                return Ok(speaker);
            }

            return BadRequest("This speaker does not exist.");
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateSpeakerModel speaker)
        {
            await dataAccessProvider.AddSpeaker(speaker);
            return Ok();
        }
    }
}
