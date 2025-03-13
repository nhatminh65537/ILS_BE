using ILS_BE.Domain.DTOs;
using ILS_BE.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ILS_BE.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly IDataService<LessonDTO> _lessonDataService;
        public LessonsController(IDataService<LessonDTO> lessonDataService)
        {
            _lessonDataService = lessonDataService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _lessonDataService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _lessonDataService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] LessonDTO lessonDTO)
        {
            await _lessonDataService.AddAsync(lessonDTO);
            return CreatedAtAction(nameof(Get), new { id = lessonDTO.Id }, lessonDTO);
        }

        [HttpPut]
        public async Task<ActionResult> Update(int id, [FromBody] LessonDTO lessonDTO)
        {
            if (lessonDTO.Id != id)
            {
                return BadRequest();
            }
            await _lessonDataService.UpdateAsync(lessonDTO);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            await _lessonDataService.DeleteAsync(id);
            return NoContent();
        }
    }
}
