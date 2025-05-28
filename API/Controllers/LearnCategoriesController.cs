using ILS_BE.Application.Interfaces;
using ILS_BE.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ILS_BE.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LearnCategoriesController : ControllerBase
    {
        private readonly IDataService<LearnCategoryDTO> _categoryDataService;
        public LearnCategoriesController(IDataService<LearnCategoryDTO> categoryDataService)
        {
            _categoryDataService = categoryDataService;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _categoryDataService.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _categoryDataService.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] LearnCategoryDTO categoryDTO)
        {
            await _categoryDataService.AddAsync(categoryDTO);
            return CreatedAtAction(nameof(Get), new { id = categoryDTO.Id }, categoryDTO);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] LearnCategoryDTO categoryDTO)
        {
            if (categoryDTO.Id != id)
            {
                return BadRequest();
            }
            await _categoryDataService.UpdateAsync(categoryDTO);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _categoryDataService.DeleteAsync(id);
            return NoContent();
        }
    }
}
