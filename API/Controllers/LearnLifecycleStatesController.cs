using ILS_BE.Application.Interfaces;
using ILS_BE.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ILS_BE.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LearnLifecycleStatesController : ControllerBase
    {
        private readonly IDataService<LearnLifecycleStateDTO> _dataService;
        public LearnLifecycleStatesController(IDataService<LearnLifecycleStateDTO> dataService)
        {
            _dataService = dataService;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _dataService.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _dataService.GetByIdAsync(id));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, LearnLifecycleStateDTO lifecycleState)
        {
            if (id != lifecycleState.Id)
            {
                return BadRequest();
            }
            await _dataService.UpdateAsync(lifecycleState);
            return NoContent();
        }
    }
}
