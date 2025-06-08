using ILS_BE.Application.Services;
using ILS_BE.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ILS_BE.API.Controllers
{
    [ApiController]
    [Route("api/v1/flag")]
    [Authorize]
    public class CheckFlagController : ControllerBase
    {
        private readonly FlagCheckerService _flagCheckerService;

        public CheckFlagController(FlagCheckerService flagCheckerService)
        {
            _flagCheckerService = flagCheckerService;
        }

        /// <summary>
        /// Check submitted flag for a challenge.
        /// </summary>
        [HttpPost("check")]
        public async Task<IActionResult> CheckFlag([FromBody] FlagCheckerSubmitDTO request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Flag))
                return BadRequest(new { message = "Flag is required." });

            var result = await _flagCheckerService.CheckFlagAsync(request);
            return Ok(result);
        }
    }
}
