using ILS_BE.Application.Authorization;
using ILS_BE.Application.Interfaces;
using ILS_BE.Application.Services;
using ILS_BE.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ILS_BE.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class LearnNodesController : ControllerBase
    {
        private readonly LearnNodeService _learnNodeDataService;
        public LearnNodesController(LearnNodeService learnNodeDataService)
        {
            _learnNodeDataService = learnNodeDataService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _learnNodeDataService.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _learnNodeDataService.GetByIdAsync(id));
        }

        [PermissionAuthorize]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] LearnNodeCreateOrUpdateDTO contentItemDto)
        {
            var returnContentItemDto = await _learnNodeDataService.AddAsync(contentItemDto);
            return CreatedAtAction(nameof(Get), new { id = returnContentItemDto.Id }, returnContentItemDto);
        }

        [PermissionAuthorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] LearnNodeCreateOrUpdateDTO contentItemDTO)
        {
            if (contentItemDTO.Id != id)
            {
                return BadRequest();
            }
            await _learnNodeDataService.UpdateAsync(contentItemDTO);
            return NoContent();
        }

        [PermissionAuthorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _learnNodeDataService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}/tree")]
        public async Task<ActionResult> GetTree(int id)
        {
            return Ok(await _learnNodeDataService.GetTreeByIdAsync(id));
        }

        [HttpPut("{id}/tree")]
        public async Task<ActionResult> UpdateTree(int id, [FromBody] TreeDTO<LearnNodeDTO> moduleDetailDTO)
        {
            if (moduleDetailDTO.Item.Id != id)
            {
                return BadRequest();
            }
            await _learnNodeDataService.UpdateTreeAsync(moduleDetailDTO);
            return NoContent();
        }
    }
}
