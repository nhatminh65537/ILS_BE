using ILS_BE.Domain.DTOs;
using ILS_BE.Application.Services;
using ILS_BE.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ILS_BE.Application.Authorization;

namespace ILS_BE.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _dataService;
        public RolesController(IRoleService serviceProvider)
        {
            _dataService = serviceProvider;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _dataService.GetAllAsync());
        }

        [HttpGet("{roleId}")]
        public async Task<ActionResult> Get(int roleId)
        {
            return Ok(await _dataService.GetByIdAsync(roleId));
        }

        [PermissionAuthorize]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] RoleDTO roleDto)
        {
            roleDto = await _dataService.AddAsync(roleDto);
            return CreatedAtAction(nameof(Get), new { RoleId = roleDto.Id }, roleDto);
        }

        [HttpPut("{roleId}")]
        public async Task<ActionResult> Update(int roleId, [FromBody] RoleDTO roleDTO)
        {
            if (roleDTO.Id != roleId)
            {
                return BadRequest();
            }
            await _dataService.UpdateAsync(roleDTO);
            return NoContent();
        }

        [HttpDelete("{roleId}")]
        public async Task<ActionResult> Delete(int roleId)
        {
            await _dataService.DeleteAsync(roleId);
            return NoContent();
        }

        [HttpGet("{roleId}/permissions")]
        public async Task<ActionResult> GetPermissions(int roleId)
        {
            var permissions = await _dataService.GetPermissionsInRoleAsync(roleId);
            return Ok(permissions);
        }

        [HttpPost("{roleId}/permissions/{permissionId}")]
        public async Task<ActionResult> AddPermission(int roleId, int permissionId)
        {
            await _dataService.AddPermissionToRoleAsync(roleId, permissionId);
            return NoContent();
        }

        [HttpDelete("{roleId}/permissions/{permissionId}")]
        public async Task<ActionResult> RemovePermission(int roleId, int permissionId)
        {
            await _dataService.RemovePermissionFromRoleAsync(roleId, permissionId);
            return NoContent();
        }
    }
}
