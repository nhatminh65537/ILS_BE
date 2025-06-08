using ILS_BE.Application.Interfaces;
using ILS_BE.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ILS_BE.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class LearnProgressStatesController : ControllerBase
    {
        private readonly IDataService<LearnProgressStateDTO> _progressStateDataService;
        public LearnProgressStatesController(IDataService<LearnProgressStateDTO> progressStateDataService)
        {
            _progressStateDataService = progressStateDataService;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _progressStateDataService.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _progressStateDataService.GetByIdAsync(id));
        }
        [HttpPut]
        public async Task<ActionResult> Update(int id, [FromBody] LearnProgressStateDTO progressStateDTO)
        {
            if (progressStateDTO.Id != id)
            {
                return BadRequest();
            }
            await _progressStateDataService.UpdateAsync(progressStateDTO);
            return NoContent();
        }
    }
}
