using System.Net;
using System.Security.Claims;
using DATINGWEBAPI.BAL.Services.IServices;
using DATINGWEBAPI.BAL.Utilities.Common;
using DATINGWEBAPI.DTO.DTOs;
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
    public class NotificationController : ControllerBase
    {
        private readonly ILogger<NotificationController> _logger;
        private readonly IStringLocalizer<ResponseMessage> _localizer;
        private readonly INotificationService _notificationService;

        public NotificationController(
            ILogger<NotificationController> logger,
            IStringLocalizer<ResponseMessage> localizer,
            INotificationService notificationService
        )
        {
            _logger = logger;
            _localizer = localizer;
            _notificationService = notificationService;
        }

        /// <summary>
        /// Get all notifications for the currently logged-in user.
        /// </summary>
        /// <remarks>
        /// Retrieves a list of notifications specific to the authenticated user.
        /// </remarks>
        /// <returns>A list of user notifications.</returns>
        [SwaggerOperation(Summary = "Get user notifications", Description = "Retrieves all notifications for the currently logged-in user.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Notifications retrieved successfully", typeof(ResponseData))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access - user is not logged in")]
        [HttpGet("get-user-notifications")]
        public async Task<IActionResult> GetUserNotifications()
        {
            var userId = Convert.ToInt64(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _notificationService.GetUserNotificationsAsync(userId);
            return Ok(new ResponseData
            {
                Code = (int)HttpStatusCode.OK,
                Message = _localizer[nameof(ResponseMessage.Success)],
                Status = true,
                Data = result
            });
        }

        /// <summary>
        /// Create a new notification for the currently logged-in user.
        /// </summary>
        /// <remarks>
        /// This API allows creating a new notification entry for the authenticated user.
        /// </remarks>
        /// <param name="notificationDTO">Notification data</param>
        /// <returns>The created notification details.</returns>
        [SwaggerOperation(Summary = "Create notification", Description = "Creates a new notification for the currently logged-in user.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Notification created successfully", typeof(ResponseData))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request payload")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access - user is not logged in")]
        [HttpPost("create-notification")]
        public async Task<IActionResult> CreateNotification([FromBody] NotificationRequestDTO notificationDTO)
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
            var result = await _notificationService.CreateNotificationAsync(userId, notificationDTO);
            return Ok(new ResponseData
            {
                Code = (int)HttpStatusCode.OK,
                Message = _localizer[nameof(ResponseMessage.Success)],
                Status = true,
                Data = result
            });
        }

        /// <summary>
        /// Mark a specific notification as read.
        /// </summary>
        /// <remarks>
        /// This endpoint updates the `IsRead` status of a specific notification to `true`.
        /// </remarks>
        /// <param name="notificationId">Notification ID to mark as read</param>
        /// <returns>Status of the update operation.</returns>
        [SwaggerOperation(Summary = "Mark notification as read", Description = "Marks a specific notification as read for the logged-in user.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Notification marked as read", typeof(ResponseData))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access - user is not logged in")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Notification not found")]
        [HttpPatch("{notificationId}/read")]
        public async Task<IActionResult> MarkAsRead(long notificationId)
        {
            var userId = Convert.ToInt64(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _notificationService.MarkAsReadAsync(userId, notificationId);
            return Ok(new ResponseData
            {
                Code = (int)HttpStatusCode.OK,
                Message = _localizer[nameof(ResponseMessage.Success)],
                Status = true,
                Data = result
            });
        }
    }
}
