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




    



    }
}
