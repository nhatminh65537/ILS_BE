using ILS_BE.Domain.DTOs;
using ILS_BE.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ILS_BE.Application.Services.DataServices;
using ILS_BE.Application.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace ILS_BE.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class LearnLessonsController : ControllerBase
    {
        private readonly LearnLessonService _lessonService;
        public LearnLessonsController(LearnLessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _lessonService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _lessonService.GetByIdAsync(id));
        }

        [PermissionAuthorize]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] LearnLessonCreateOrUpdateDTO lessonDTO)
        {
            await _lessonService.AddAsync(lessonDTO);
            return CreatedAtAction(nameof(Get), new { id = lessonDTO.Id }, lessonDTO);
        }

        [PermissionAuthorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] LearnLessonCreateOrUpdateDTO lessonDTO)
        {
            if (lessonDTO.Id != id)
            {
                return BadRequest();
            }
            await _lessonService.UpdateAsync(lessonDTO);
            return NoContent();
        }

        [PermissionAuthorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _lessonService.DeleteAsync(id);
            return NoContent();
        }
    }
}
