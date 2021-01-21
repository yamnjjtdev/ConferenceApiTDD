using ConferenceApiTDD.Lib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ConferenceApiTDD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeakerController : ControllerBase
    {
        private readonly ILogger<SpeakerController> _logger;
        private readonly IConferenceApiService _conferenceService;

        public SpeakerController(ILogger<SpeakerController> logger, IConferenceApiService conferenceService)
        {
            _logger = logger;
            _conferenceService = conferenceService;
        }

        [HttpGet("ReadSessionsWithTopics")]
        public async Task<IActionResult> ReadSessionsWithTopics(
            string speakerName,
            DateTime? datetime)
        {
            _logger.LogInformation($"Get requested with {speakerName} {datetime}");

            if (string.IsNullOrEmpty(speakerName))
                return BadRequest("Speaker name is missing");
            try
            {
                var speakerObj = await _conferenceService.FindSpeaker(speakerName);

                if (speakerObj == null) return BadRequest("Speaker not found.");

                var speaker = await _conferenceService.ReadSessionsWithTopics(speakerObj, datetime);

                _logger.LogInformation($"Information retrieved", speaker);
                return Ok(speaker);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e, "Exception while reading speaker session and topic information");
                return StatusCode(500, e.Message);
            }
        }
    }

}
