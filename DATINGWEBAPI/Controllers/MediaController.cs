using System.Net;
using System.Security.Claims;
using DATINGWEBAPI.BAL.Services.IServices;
using DATINGWEBAPI.BAL.Utilities.Common;
using DATINGWEBAPI.DTO.RequestDTO;
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
    public class MediaController : ControllerBase
    {
        private readonly ILogger<MediaController> _logger;
        private readonly IStringLocalizer<ResponseMessage> _localizer;
        private readonly ICloudinaryService _cloudinaryService;

        public MediaController(
            ILogger<MediaController> logger,
            IStringLocalizer<ResponseMessage> localizer,
            ICloudinaryService cloudinaryService

            )
        {
            _logger = logger;
            _localizer = localizer;
            _cloudinaryService = cloudinaryService;
        }

        /// <summary>
        /// Get media images for the currently logged-in user.
        /// </summary>
        /// <remarks>
        /// Returns a list of media images (uploaded via Cloudinary) for the authenticated user.
        /// </remarks>
        /// <returns>List of user's media images wrapped in a response object.</returns>
        [SwaggerOperation(Summary = "Get media images",Description = "Retrieves all uploaded media images for the currently logged-in user from Cloudinary.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Media images retrieved successfully", typeof(ResponseData))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request or validation failure")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access - user is not logged in")]
        [HttpGet("get-media-images")]
        public async Task<IActionResult> getMediaImages()
        {
            var userId = Convert.ToInt64(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _cloudinaryService.getMediaImages(userId);
            return Ok(new ResponseData
            {
                Code = (int)HttpStatusCode.OK,
                Message = _localizer[nameof(ResponseMessage.Success)],
                Status = true,
                Data = result
            });
        }

        /// <summary>
        /// Upload media image (max 5MB)
        /// </summary>
        [SwaggerOperation(Summary = "Upload media image",Description = "Uploads an image (max 5MB) to Cloudinary for the currently logged-in user")]
        [SwaggerResponse(StatusCodes.Status200OK, "Upload successful", typeof(ResponseData))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Validation failed")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
        [HttpPost("add-media-images")]
        [RequestSizeLimit(5 * 1024 * 1024)] // 5 MB limit
        public async Task<IActionResult> AddMediaImages([FromForm] MediaUploadRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new ResponseData
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = _localizer[nameof(ResponseMessage.Fail)],
                    Status = false,
                    Data = errors
                });
            }

            if (request.File.Length > 5 * 1024 * 1024)
            {
                return BadRequest(new ResponseData
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = "Maximum file size exceeded (5 MB)",
                    Status = false
                });
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var extension = Path.GetExtension(request.File.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest(new ResponseData
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = "Invalid file type. Only JPG, PNG, WEBP are allowed.",
                    Status = false
                });
            }

            var userId = Convert.ToInt64(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _cloudinaryService.AddMediaImages(userId, request);

            return Ok(new ResponseData
            {
                Code = (int)HttpStatusCode.OK,
                Message = _localizer[nameof(ResponseMessage.Success)],
                Status = true,
                Data = result
            });
        }

        /// <summary>
        /// Delete a media image uploaded by the currently logged-in user.
        /// </summary>
        /// <param name="mediaId">The unique identifier of the media image to delete.</param>
        /// <remarks>
        /// Deletes the specified media image from Cloudinary and the database if it belongs to the current user.
        /// </remarks>
        /// <returns>Success status wrapped in a response object.</returns>
        [SwaggerOperation(Summary = "Delete media image",Description = "Deletes a specific media image uploaded by the currently logged-in user.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Image deleted successfully", typeof(ResponseData))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid media ID or request")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized - user must be logged in")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Media image not found or does not belong to the user")]
        [HttpDelete("delete-media-image/{mediaId}")]
        public async Task<IActionResult> DeleteMediaImage(string mediaId)
        {
            var userId = Convert.ToInt64(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _cloudinaryService.DeleteMediaImage(userId, mediaId);

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
