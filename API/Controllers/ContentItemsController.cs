using ILS_BE.Application.Interfaces;
using ILS_BE.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ILS_BE.API.Controllers
{
    [ApiController]
    [Route("api/v1/content-items")]
    public class ContentItemsController : ControllerBase
    {
        private readonly IContentItemService _contentItemDataService;
        public ContentItemsController(IContentItemService contentItemDataService)
        {
            _contentItemDataService = contentItemDataService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _contentItemDataService.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _contentItemDataService.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ContentItemDTO contentItemDto)
        {
            contentItemDto = await _contentItemDataService.AddAsync(contentItemDto);
            return CreatedAtAction(nameof(Get), new { id = contentItemDto.Id }, contentItemDto);
        }
        [HttpPut]
        public ActionResult Update(int id, [FromBody] ContentItemDTO contentItemDTO)
        {
            if (contentItemDTO.Id != id)
            {
                return BadRequest();
            }
            _contentItemDataService.UpdateAsync(contentItemDTO);
            return NoContent();
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            _contentItemDataService.DeleteAsync(id);
            return NoContent();
        }
    }
}
