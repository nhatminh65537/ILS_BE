// API/Controllers/ChallengeNodeController.cs
using Microsoft.AspNetCore.Mvc;
using ILS_BE.Domain.DTOs;
using ILS_BE.Application.Services.DataServices;
using Microsoft.AspNetCore.Authorization;
using ILS_BE.Application.Authorization;

namespace ILS_BE.API.Controllers
{
    [ApiController]
    [Route("api/v1/challengenodes")]
    [Authorize]
    public class ChallengeNodeController : ControllerBase
    {
        private readonly ChallengeNodeService _service;

        public ChallengeNodeController(ChallengeNodeService service)
        {
            _service = service;
        }

        // GET: paginated with filters
        [HttpPost("{id}")]
        public async Task<IActionResult> GetPaginated(int id, [FromBody] ChallengeNodeFilterDTO filterDTO)
        {
            if (filterDTO.ParentNodeId != id)
                return BadRequest("Parent node ID does not match the route ID.");
            var result = await _service.GetPaginatedAsync(filterDTO);
            return Ok(result);
        }

        // GET: user relative filter (isSolve)
        //[HttpGet("user/{userId}/solved")]
        //public async Task<IActionResult> GetNodesWithUserSolve(int userId)
        //{
        //    var result = await _service.GetNodesWithUserSolve(userId);
        //    return Ok(result);
        //}

        // POST: create node
        [PermissionAuthorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ChallengeNodeCreateOrUpdateDTO dto)
        {
            var result = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        // GET: get node by id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var node = await _service.GetByIdAsync(id);
            return node != null ? Ok(node) : NotFound();
        }

        // PUT: update node
        [PermissionAuthorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ChallengeNodeCreateOrUpdateDTO dto)
        {
            if (dto.Id != id)
                return BadRequest();
            await _service.UpdateAsync(dto);
            return NoContent();
        }

        // DELETE: delete node and all subnodes/problems
        [PermissionAuthorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
