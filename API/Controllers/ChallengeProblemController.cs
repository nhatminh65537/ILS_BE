// API/Controllers/ChallengeProblemController.cs
using Microsoft.AspNetCore.Mvc;
using ILS_BE.Domain.DTOs;
using ILS_BE.Application.Services.DataServices;

namespace ILS_BE.API.Controllers
{
    [ApiController]
    [Route("api/v1/challengeproblems")]
    public class ChallengeProblemController : ControllerBase
    {
        private readonly ChallengeProblemService _service;

        public ChallengeProblemController(ChallengeProblemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var problem = await _service.GetByIdAsync(id);
            return problem != null ? Ok(problem) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ChallengeProblemCreateOrUpdateDTO dto)
        {
            var result = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ChallengeProblemCreateOrUpdateDTO dto)
        {
            if (dto.Id != id)
                return BadRequest();
            await _service.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        [HttpPost("{id}/file")]
        public async Task<IActionResult> UploadFile(int id, ChallengeFileDTO dto)
        {
            if (id != dto.ChallengeId)
                return BadRequest("Challenge ID does not match the route ID.");
            await _service.UploadFileAsync(dto);
            return Ok();
        }
        [HttpDelete("{id}/file/{fileId}")]
        public async Task<IActionResult> UpdateFile(int id, int fileId)
        {
            await _service.DeleteFileAsync(fileId);
            return NoContent();
        }
    }
}
