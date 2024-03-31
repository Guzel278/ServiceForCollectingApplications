using System;
using Microsoft.AspNetCore.Mvc;
using ServiceForCollectingApplications.Models;
using ServiceForCollectingApplications.Services;

namespace ServiceForCollectingApplications.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantService _participantService;

        public ParticipantController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        //Cоздание участника
        [HttpPost]
        public async Task<IActionResult> CreateParticipant([FromBody] Participants participant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdParticipant = await _participantService.CreateParticipantAsync(participant);
            //return CreatedAtAction(nameof(GetParticipant), new { id = createdParticipant.ParticipantId }, createdParticipant);
            return Ok(createdParticipant);
        }
    }
}

