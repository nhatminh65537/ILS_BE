using ILS_BE.Application.Interfaces;
using ILS_BE.Domain.DTOs;
using ILS_BE.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ILS_BE.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PermissionsController : ControllerBase
    {
        private readonly IDataService<PermissionDTO> _dataService;
        public PermissionsController(IDataService<PermissionDTO> dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _dataService.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _dataService.GetByIdAsync(id));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, PermissionDTO permission)
        {
            if (id != permission.Id)
            {
                return BadRequest();
            }
            await _dataService.UpdateAsync(permission);
            return NoContent();
        }
    }
}
