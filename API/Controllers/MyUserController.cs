using ILS_BE.Domain.DTOs;
using ILS_BE.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ILS_BE.Domain.Models;

namespace ILS_BE.API.Controllers
{
    [Route("api/v1/my-user")]
    [ApiController]
    [Authorize]
    public class MyUserController : ControllerBase
    {
        private readonly IMyUserService _myUserService;
        public MyUserController(IMyUserService serviceProvider)
        {
            _myUserService = serviceProvider;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var user = await _myUserService.GetMyUser();
            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UserDetailDTO myUserDTO)
        {
            await _myUserService.UpdateMyUserAsync(myUserDTO);
            return NoContent();
        }

        [HttpGet("permissions")]
        public async Task<ActionResult> GetPermissions()
        {
            var permissions = await _myUserService.GetPermissionsInMyUserAsync();
            return Ok(permissions);
        }
        [HttpGet("roles")]
        public async Task<ActionResult> GetRoles()
        {
            var roles = await _myUserService.GetRolesInMyUserAsync();
            return Ok(roles);
        }
        [HttpGet("profile")]
        public async Task<ActionResult> GetProfile()
        {
            var profile = await _myUserService.GetUserProfileInMyUserAsync();
            return Ok(profile);
        }
        [HttpGet("progress-states")]
        public async Task<ActionResult> GetModuleProgress()
        {
            var progressStates = await _myUserService.GetModuleProgressAsync();
            return Ok(progressStates);
        }
        [HttpPut("modules/{id}/progress-state")]
        public async Task<ActionResult> UpdateLearnModuleProgress(int id, [FromBody] UserModuleProgressCreateOrUpdateDTO progressDTO)
        {
            if (progressDTO.ModuleId != id)
            {
                return BadRequest("Module ID mismatch");
            }
            await _myUserService.UpdateLearnModuleProgress(progressDTO);
            return NoContent();
        }
        [HttpPost("lessons/{id}")]
        public async Task<ActionResult> UpdateLearnLessonFinish(int id)
        {
            await _myUserService.UpdateLearnLessonFinish(id);
            return NoContent();
        }
        [HttpGet("modules/{moduleId}/lesson-finished")]
        public async Task<ActionResult<List<int>>> GetLessonFinish(int moduleId)
        {
            var lessons = await _myUserService.GetLessonFinishAsync(moduleId);
            return Ok(lessons);
        }
    }
}
