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
    public class StoriesController : ControllerBase
    {
        private readonly ILogger<StoriesController> _logger;
        private readonly IStringLocalizer<ResponseMessage> _localizer;
        private readonly ICloudinaryService _cloudinaryService;

        public StoriesController(
            ILogger<StoriesController> logger,
            IStringLocalizer<ResponseMessage> localizer,
            ICloudinaryService cloudinaryService

            )
        {
            _logger = logger;
            _localizer = localizer;
            _cloudinaryService = cloudinaryService;
        }

        /// <summary>
        /// Uploads a user story media file (image or video, max size: 10 MB).
        /// </summary>
        [SwaggerOperation(Summary = "Upload story media (image/video)",Description = "Uploads an image or video (maximum size: 10 MB) to Cloudinary for the currently logged-in user. " + "Supported image formats: JPG, JPEG, PNG, WEBP. "+ "Supported video formats: MP4, MOV, WEBM.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Upload successful", typeof(ResponseData))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid file format or validation failed")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized access")]
        [HttpPost("add-story")]
        [RequestSizeLimit(10 * 1024 * 1024)] // 10 MB
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddStory([FromForm] CreateStoryRequestDTO request)
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

            if (request.MediaFile.Length > 10 * 1024 * 1024)
            {
                return BadRequest(new ResponseData
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = "Maximum file size exceeded (10 MB)",
                    Status = false
                });
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp", ".mp4", ".mov", ".webm" };
            var extension = Path.GetExtension(request.MediaFile.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest(new ResponseData
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = "Invalid file type. Only JPG, JPEG, PNG, WEBP (images) and MP4, MOV, WEBM (videos) are allowed.",
                    Status = false
                });
            }

            var userId = Convert.ToInt64(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _cloudinaryService.AddStory(userId, request);

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
