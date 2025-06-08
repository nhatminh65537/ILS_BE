using ILS_BE.Application.Authorization;
using ILS_BE.Application.Interfaces;
using ILS_BE.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ILS_BE.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class LearnLessonTypesController : ControllerBase
    {
        private readonly IDataService<LearnLessonTypeDTO> _lessonTypeDataService;
        public LearnLessonTypesController(IDataService<LearnLessonTypeDTO> lessonTypeDataService)
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

        [PermissionAuthorize]
        [HttpPut]
        public async Task<ActionResult> Update(int id, [FromBody] LearnLessonTypeDTO lessonTypeDTO)
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
