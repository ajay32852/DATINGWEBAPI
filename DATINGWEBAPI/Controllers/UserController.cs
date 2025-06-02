using System.Net;
using System.Security.Claims;
using DATINGWEBAPI.BAL.Services.IServices;
using DATINGWEBAPI.BAL.Utilities.Common;
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

        [SwaggerOperation(Summary = "Get User Detail  ", Description = "Get Current User Detail")]
        [SwaggerResponse(200, "OTP Sent Successfully")]
        [SwaggerResponse(400, "Invalid Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [HttpGet("user-detail")]
        public async Task<IActionResult> UserDetail()
        {
            var userId = Convert.ToInt64(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var data = await _userService.getUserDetailByUserId(userId);
            return Ok(new ResponseData
            {
                Code = Convert.ToInt16(HttpStatusCode.OK),
                Message = _localizer[name: ResponseMessage.Success.ToString()].Value,
                Status = true,
                Data = data
            });

        }




    }
}
