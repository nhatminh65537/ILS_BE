// API/Controllers/ChallengeCategoryController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ILS_BE.Domain.DTOs;
using ILS_BE.Application.Services.DataServices;
using ILS_BE.Application.Interfaces;
using ILS_BE.Application.Authorization;

namespace ILS_BE.API.Controllers
{
    [ApiController]
    [Route("api/v1/challengecategories")]
    [Authorize]
    public class ChallengeCategoryController : ControllerBase
    {
        private readonly IDataService<ChallengeCategoryDTO> _challengeCategoryService;

        public ChallengeCategoryController(IDataService<ChallengeCategoryDTO> challengeCategoryService)
        {
            _challengeCategoryService = challengeCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _challengeCategoryService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var category = await _challengeCategoryService.GetByIdAsync(id);
            return category != null ? Ok(category) : NotFound();
        }

        [PermissionAuthorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ChallengeCategoryDTO challengeCategoryDTO)
        {
            var result = await _challengeCategoryService.AddAsync(challengeCategoryDTO);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [PermissionAuthorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ChallengeCategoryDTO challengeCategoryDTO)
        {
            if (id != challengeCategoryDTO.Id)
            {
                return BadRequest();
            }

            await _challengeCategoryService.UpdateAsync(challengeCategoryDTO);
            return NoContent();
        }

        [PermissionAuthorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _challengeCategoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
