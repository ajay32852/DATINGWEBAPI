using System.Net;
using System.Security.Claims;
using DATINGWEBAPI.BAL.Services;
using DATINGWEBAPI.BAL.Services.IServices;
using DATINGWEBAPI.BAL.Utilities.Common;
using DATINGWEBAPI.DTO.DTOs;
using DATINGWEBAPI.DTO.RequestDTO;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace DATINGWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DiscoverController : ControllerBase
    {
        private readonly ILogger<DiscoverController> _logger;
        private readonly IStringLocalizer<ResponseMessage> _localizer;
        private readonly ISwipeService _swipeService;

        public DiscoverController(
            ILogger<DiscoverController> logger,
            IStringLocalizer<ResponseMessage> localizer,
            ISwipeService swipeService
            )
        {
            _logger = logger;
            _localizer = localizer;
            _swipeService= swipeService;
        }


        /// <summary>
        /// Retrieves a paginated list of user profiles not yet swiped by the logged-in user.
        /// </summary>
        [SwaggerOperation(Summary = "Get swipeable profiles",Description = "Retrieves a paginated list of user profiles that the logged-in user has not yet swiped on.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successfully retrieved swipeable profiles", typeof(ResponseData))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid pagination parameters")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access")]
        [HttpGet("Swipe-profile")]
        public async Task<IActionResult> SwipeProfile([FromQuery] PaginationParams pagination)
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
                    Message = _localizer[nameof(ResponseMessage.Fail)],
                    Status = false,
                    Data = errors
                });
            }

            var userId = Convert.ToInt64(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var data = await _swipeService.SwipeProfile(userId, pagination.PageNumber, pagination.PageSize);

            return Ok(new ResponseData
            {
                Code = Convert.ToInt16(HttpStatusCode.OK),
                Message = _localizer[nameof(ResponseMessage.Success)],
                Status = true,
                Data = data
            });
        }
        /// <summary>
        /// Swipe (like/dislike) a user profile. The swiper is the currently logged-in user.
        /// </summary>
        [SwaggerOperation(Summary = "Swipe a user profile",Description = "Allows the currently logged-in user to like or dislike another user's profile.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Swipe action completed successfully", typeof(ResponseData))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid swipe request data")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized: User is not logged in")]
        [HttpPost("save-swipe-profile")]
        public async Task<IActionResult> SwipeProfile([FromBody] SwipeRequestDTO request)
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

            var userId = Convert.ToInt64(User.FindFirstValue(ClaimTypes.NameIdentifier)); // Use long to match entity
            var result = await _swipeService.SaveSwipeProfile(userId, request);

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
