﻿using ILS_BE.Application.Interfaces;
using ILS_BE.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ILS_BE.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LearnTagsController : ControllerBase
    {
        private readonly IDataService<LearnTagDTO> _tagDataService;
        public LearnTagsController(IDataService<LearnTagDTO> tagDataService)
        {
            _tagDataService = tagDataService;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _tagDataService.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _tagDataService.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] LearnTagDTO tagDTO)
        {
            tagDTO = await _tagDataService.AddAsync(tagDTO);
            return CreatedAtAction(nameof(Get), new { id = tagDTO.Id }, tagDTO);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] LearnTagDTO tagDTO)
        {
            if (tagDTO.Id != id)
            {
                return BadRequest();
            }
            await _tagDataService.UpdateAsync(tagDTO);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _tagDataService.DeleteAsync(id);
            return NoContent();
        }
    }
}
