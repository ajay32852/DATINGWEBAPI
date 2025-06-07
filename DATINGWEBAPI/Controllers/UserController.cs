using System.Net;
using System.Security.Claims;
using DATINGWEBAPI.BAL.Services.IServices;
using DATINGWEBAPI.BAL.Utilities.Common;
using DATINGWEBAPI.DTO.RequestDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace DATINGWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
     

        private readonly ILogger<UserController> _logger;
        private readonly IStringLocalizer<ResponseMessage> _localizer;
        private readonly IUserService _userService;

        public UserController(
            ILogger<UserController> logger,
            IStringLocalizer<ResponseMessage> localizer,
            IUserService userService
            )
        {
            _logger = logger;
            _localizer = localizer;
            _userService= userService;
        }


        [SwaggerOperation(Summary = "Get User Profile",Description = "Retrieves the currently logged-in user's profile based on the user ID from the JWT token.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successfully retrieved user profile", typeof(ResponseData))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access")]
        [HttpGet("user-profile")]
        public async Task<IActionResult> GetUserDetailByUserId()
        {
            var userId = Convert.ToInt16(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var data = await _userService.getUserDetailByUserId(userId);
            return Ok(new ResponseData
            {
                Code = Convert.ToInt16(HttpStatusCode.OK),
                Message = _localizer[ResponseMessage.Success.ToString()].Value,
                Status = true,
                Data = data
            });
        }

        [SwaggerOperation(Summary = "Update User Profile", Description = "Updates the profile of the currently logged-in user.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Profile updated successfully", typeof(ResponseData))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request data")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "User is not authorized")]
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserProfileRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new ResponseData
                {
                    Code = Convert.ToInt16(HttpStatusCode.BadRequest),
                    Message = _localizer[nameof(ResponseMessage.Fail)].Value,
                    Status = false,
                    Data = errors
                });
            }

            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _userService.UpdateUserProfileAsync(userId, request);
            return Ok(new ResponseData
            {
                Code = Convert.ToInt16(HttpStatusCode.OK),
                Message = _localizer[nameof(ResponseMessage.Success)].Value,
                Status = true,
                Data = result
            });
        }









    }
}
