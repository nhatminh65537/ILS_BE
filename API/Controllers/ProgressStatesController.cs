using ILS_BE.Application.Interfaces;
using ILS_BE.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ILS_BE.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProgressStatesController : ControllerBase
    {
        private readonly IDataService<ProgressStateDTO> _progressStateDataService;
        public ProgressStatesController(IDataService<ProgressStateDTO> progressStateDataService)
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
        public async Task<ActionResult> Update(int id, [FromBody] ProgressStateDTO progressStateDTO)
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
