using ILS_BE.Application.Interfaces;
using ILS_BE.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ILS_BE.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LessonTypesController : ControllerBase
    {
        private readonly IDataService<LessonTypeDTO> _lessonTypeDataService;
        public LessonTypesController(IDataService<LessonTypeDTO> lessonTypeDataService)
        {
            _lessonTypeDataService = lessonTypeDataService;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _lessonTypeDataService.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _lessonTypeDataService.GetByIdAsync(id));
        }
        [HttpPut]
        public async Task<ActionResult> Update(int id, [FromBody] LessonTypeDTO lessonTypeDTO)
        {
            if (lessonTypeDTO.Id != id)
            {
                return BadRequest();
            }
            await _lessonTypeDataService.UpdateAsync(lessonTypeDTO);
            return NoContent();
        }
    }
}
