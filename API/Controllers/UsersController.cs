using ILS_BE.Domain.DTOs;
using ILS_BE.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using ILS_BE.Application.Authorization;

namespace ILS_BE.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService serviceProvider)
        {
            _userService = serviceProvider;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _userService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await _userService.GetByIdAsync(id));
        }

        // Not add User like this (User AuthController [register] instead)
        [PermissionAuthorize]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] UserDTO userDTO)
        {
            userDTO = await _userService.AddAsync(userDTO);
            return CreatedAtAction(nameof(Get), new { id = userDTO.Id }, userDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UserDTO userDTO)
        {
            if (userDTO.Id != id)
            {
                return BadRequest();
            }
            await _userService.UpdateAsync(userDTO);
            return NoContent();
        }

        // Not delete User like this (User AuthController [unregister] instead)
        [PermissionAuthorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}/profile")]
        public async Task<ActionResult> GetProfile(int id)
        {
            var profile = await _userService.GetUserProfileAsync(id);
            return Ok(profile);
        }

        [HttpGet("{id}/permissions")]
        public async Task<ActionResult> GetPermissions(int id)
        {
            var permissions = await _userService.GetEffectivePermissionsOfUserAsync(id);
            return Ok(permissions);
        }

        [HttpPost("{id}/permissions/{permissionId}")]
        public async Task<ActionResult> AddPermission(int id, int permissionId)
        {
            await _userService.AddPermissionToUserAsync(id, permissionId);
            return NoContent();
        }

        [HttpDelete("{id}/permissions/{permissionId}")]
        public async Task<ActionResult> RemovePermission(int id, int permissionId)
        {
            await _userService.RemovePermissionFromUserAsync(id, permissionId);
            return NoContent();
        }

        [HttpGet("{id}/roles")]
        public async Task<ActionResult> GetRoles(int id)
        {
            var roles = await _userService.GetRolesOfUserAsync(id);
            return Ok(roles);
        }

        [HttpPost("{id}/roles/{roleId}")]
        public async Task<ActionResult> AddRole(int id, int roleId)
        {
            await _userService.AddRoleToUserAsync(id, roleId);
            return NoContent();
        }

        [HttpDelete("{id}/roles/{roleId}")]
        public async Task<ActionResult> RemoveRole(int id, int roleId)
        {
            await _userService.RemoveRoleFromUserAsync(id, roleId);
            return NoContent();
        }
        [HttpGet("scoreboard")]
        public async Task<ActionResult> GetUserScoreboard([FromQuery] int page, [FromQuery] int pageSize)
        {
            var res = await _userService.GetPaginatedAsync(page, pageSize);
            return Ok(res);
        }
    }
}
