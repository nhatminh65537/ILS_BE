// API/Controllers/ChallengeTagController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ILS_BE.Domain.DTOs;
using ILS_BE.Application.Services.DataServices;
using ILS_BE.Application.Interfaces;

namespace ILS_BE.API.Controllers
{
    [ApiController]
    [Route("api/v1/challengetags")]
    public class ChallengeTagController : ControllerBase
    {
        private readonly IDataService<ChallengeTagDTO> _challengeTagService;

        public ChallengeTagController(IDataService<ChallengeTagDTO> challengeTagService)
        {
            _challengeTagService = challengeTagService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _challengeTagService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var tag = await _challengeTagService.GetByIdAsync(id);
            return tag != null ? Ok(tag) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ChallengeTagDTO challengeTagDTO)
        {
            var result = await _challengeTagService.AddAsync(challengeTagDTO);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ChallengeTagDTO challengeTagDTO)
        {
            if (id != challengeTagDTO.Id)
            {
                return BadRequest();
            }

            await _challengeTagService.UpdateAsync(challengeTagDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _challengeTagService.DeleteAsync(id);
            return NoContent();
        }
    }
}
