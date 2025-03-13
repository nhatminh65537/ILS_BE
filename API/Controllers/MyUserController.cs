using ILS_BE.Domain.DTOs;
using ILS_BE.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ILS_BE.API.Controllers
{
    [Route("api/v1/my-user")]
    [ApiController]
    [Authorize]
    public class MyUserController : ControllerBase
    {
        private readonly IMyUserService _dataService;
        public MyUserController(IMyUserService serviceProvider)
        {
            _dataService = serviceProvider;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var user = await _dataService.GetMyUser();
            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UserDetailDTO myUserDTO)
        {
            await _dataService.UpdateMyUserAsync(myUserDTO);
            return NoContent();
        }

        [HttpGet("permissions")]
        public async Task<ActionResult> GetPermissions()
        {
            var permissions = await _dataService.GetPermissionsInMyUserAsync();
            return Ok(permissions);
        }
        [HttpGet("roles")]
        public async Task<ActionResult> GetRoles()
        {
            var roles = await _dataService.GetRolesInMyUserAsync();
            return Ok(roles);
        }
        [HttpGet("profile")]
        public async Task<ActionResult> GetProfile()
        {
            var profile = await _dataService.GetUserProfileInMyUserAsync();
            return Ok(profile);
        }
    }
}
