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
    public class MatchController : ControllerBase
    {
        private readonly ILogger<MatchController> _logger;
        private readonly IStringLocalizer<ResponseMessage> _localizer;
        private readonly IUserService _userService;
        private readonly IUserLike _userLike;

        public MatchController(
            ILogger<MatchController> logger,
            IStringLocalizer<ResponseMessage> localizer,
            IUserLike userLike,
            IUserService userService)
        {
            _logger = logger;
            _localizer = localizer;
            _userService = userService;
            _userLike = userLike;
        }

        /// <summary>
        /// Like a user profile.
        /// </summary>
        /// <remarks>
        /// Allows the authenticated user to like another user's profile. 
        /// If a mutual like exists, it can be considered a match.
        /// </remarks>
        /// <param name="profileLikeRequest">Target user details for the like action.</param>
        [SwaggerOperation(Summary = "Like a user profile", Description = "Allows the current user to like another user.")]
        [SwaggerResponse(200, "Profile liked successfully.")]
        [SwaggerResponse(400, "Invalid request.")]
        [SwaggerResponse(401, "Unauthorized access.")]
        [HttpPost("profile-likes")]
        public async Task<IActionResult> ProfileLike([FromBody] ProfileLikeRequest profileLikeRequest)
        {
            var userId = Convert.ToInt64(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            profileLikeRequest.FromUserId = userId;

            var data = await _userLike.LikeProfileAsync(profileLikeRequest);

            return Ok(new ResponseData
            {
                Code = (int)HttpStatusCode.OK,
                Message = _localizer[ResponseMessage.Success.ToString()].Value,
                Status = true,
                Data = data
            });
        }

        /// <summary>
        /// Get list of matched user profiles.
        /// </summary>
        /// <remarks>
        /// Retrieves a list of users who have mutually liked the authenticated user.
        /// </remarks>
        [SwaggerOperation(Summary = "Get matched users", Description = "Returns a list of users who have mutually liked the current user.")]
        [SwaggerResponse(200, "Matched profiles fetched successfully.")]
        [SwaggerResponse(400, "Invalid request.")]
        [SwaggerResponse(401, "Unauthorized access.")]
        [HttpGet("profile-matches")]
        public async Task<IActionResult> ProfileMatch()
        {
            var userId = Convert.ToInt64(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var data = await _userLike.GetLikedProfilesAsync(userId); 
            return Ok(new ResponseData
            {
                Code = (int)HttpStatusCode.OK,
                Message = _localizer[ResponseMessage.Success.ToString()].Value,
                Status = true,
                Data = data
            });
        }
    }
}
