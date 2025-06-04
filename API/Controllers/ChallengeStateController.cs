// API/Controllers/ChallengeStateController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ILS_BE.Domain.DTOs;
using ILS_BE.Application.Services.DataServices;
using ILS_BE.Application.Interfaces;

namespace ILS_BE.API.Controllers
{
    [ApiController]
    [Route("api/v1/challengestates")]
    public class ChallengeStateController : ControllerBase
    {
        private readonly IDataService<ChallengeStateDTO> _challengeStateService;

        public ChallengeStateController(IDataService<ChallengeStateDTO> challengeStateService)
        {
            _challengeStateService = challengeStateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _challengeStateService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var state = await _challengeStateService.GetByIdAsync(id);
            return state != null ? Ok(state) : NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ChallengeStateDTO challengeStateDTO)
        {
            if (id != challengeStateDTO.Id)
            {
                return BadRequest();
            }

            await _challengeStateService.UpdateAsync(challengeStateDTO);
            return NoContent();
        }
    }
}
