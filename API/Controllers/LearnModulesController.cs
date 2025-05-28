using ILS_BE.Domain.DTOs;
using ILS_BE.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ILS_BE.Application.Services.DataServices;

namespace ILS_BE.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LearnModulesController : ControllerBase
    {
        private readonly LearnModuleService _moduleDataService;
        public LearnModulesController(LearnModuleService moduleDataService)
        {
            _moduleDataService = moduleDataService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string searchTerm = "", [FromQuery] List<int>? categoryIds = null, [FromQuery] List<int>? lifecycleStateIds = null, [FromQuery] List<int>? tagIds = null, [FromQuery] List<int>? ProgressStateIds = null)
        {
            var filters = new Dictionary<string, object>
            {
                { "searchTerm", searchTerm },
                { "categoryId", categoryIds ?? new List<int>() },
                { "lifecycleStateId", lifecycleStateIds ?? new List<int>() },
                { "tagId", tagIds ?? new List<int>() }
            };

            var paginatedResult = await _moduleDataService.GetPaginatedAsync(page, pageSize, filters);
            return Ok(paginatedResult);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _moduleDataService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] LearnModuleCreateOrUpdateDTO moduleCreateDTO)
        {
            var moduleDTO = await _moduleDataService.AddAsync(moduleCreateDTO);
            return CreatedAtAction(nameof(Get), new { id = moduleDTO.Id }, moduleDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] LearnModuleCreateOrUpdateDTO moduleDTO)
        {
            if (moduleDTO.Id != id)
            {
                return BadRequest();
            }
            await _moduleDataService.UpdateAsync(moduleDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _moduleDataService.DeleteAsync(id);
            return NoContent();
        }
    }
}
