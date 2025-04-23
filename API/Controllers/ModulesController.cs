using ILS_BE.Domain.DTOs;
using ILS_BE.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ILS_BE.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly IContentItemService _contentItemService;
        private readonly IPaginatedDataService<ModuleDTO> _moduleDataService;
        public ModulesController(IContentItemService contentItemService, IPaginatedDataService<ModuleDTO> moduleDataService)
        {
            _contentItemService = contentItemService;
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
        public ActionResult Get(int id)
        {
            return Ok(_moduleDataService.GetById(id));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ModuleDTO moduleDTO)
        {
            var contentItemDto = await _contentItemService.AddModuleAsync(moduleDTO);
            return CreatedAtAction(nameof(Get), new { id = contentItemDto.Module?.Id }, contentItemDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] ModuleDTO moduleDTO)
        {
            if (moduleDTO.Id != id)
            {
                return BadRequest();
            }
            await _moduleDataService.UpdateAsync(moduleDTO);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            await _moduleDataService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}/tree")]
        public async Task<ActionResult> GetTree(int id)
        {
            return Ok(await _contentItemService.GetModuleTreeByIdAsync(id));
        }

        [HttpPut("{id}/tree")]
        public async Task<ActionResult> UpdateTree(int id, [FromBody] ContentItemTreeDTO moduleDetailDTO)
        {
            if (moduleDetailDTO.Item.Module?.Id != id)
            {
                return BadRequest();
            }
            await _contentItemService.UpdateModuleTreeAsync(moduleDetailDTO);
            return NoContent();
        }
    }
}
