using EventWebAPI.DataAccess;
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
            if (id > 0)
            {
                var speaker = dataAccessProvider.GetSpeaker(id);

                if (speaker is null)
                {
                    return NotFound("This speaker does not exist.");
                }

                return Ok(speaker);
            }

            return NotFound("Id isn't correct.");
        }
    }
}
