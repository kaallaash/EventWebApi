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
        public IActionResult GetSpeakers()
        {
            return Ok(dataAccessProvider.GetSpeakers());
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var speaker = dataAccessProvider.GetSpeaker(id);

            if (speaker is not null)
            {
                return Ok(speaker);
            }

            return BadRequest("This speaker does not exist.");
        }

        [HttpPost]
        public IActionResult Add(CreateSpeakerModel speaker)
        {
            dataAccessProvider.AddSpeaker(speaker);
            return Ok();
        }
    }
}
