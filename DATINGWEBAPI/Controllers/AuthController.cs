using System.Net;
using DATINGWEBAPI.BAL.Services.IServices;
using DATINGWEBAPI.BAL.Utilities.Common;
using DATINGWEBAPI.DTO.DTOs;
using DATINGWEBAPI.DTO.RequestDTO;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace DATINGWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IValidator<RequestOTPDTO> _requestOTPValidator;
        private readonly IValidator<LoginRequestDTO> _loginValidator;

        private readonly ILogger<AuthController> _logger;
        private readonly IStringLocalizer<ResponseMessage> _localizer;
        private readonly IAuthService _authService;

        public AuthController(
            IValidator<RequestOTPDTO> requestOTPValidator,
            ILogger<AuthController> logger,
            IStringLocalizer<ResponseMessage> localizer,
            IValidator<LoginRequestDTO> loginValidator,
            IAuthService authService)
        {
            _requestOTPValidator = requestOTPValidator;
            _loginValidator = loginValidator;
            _logger = logger;
            _localizer = localizer;
            _authService = authService;
        }
       
        [SwaggerOperation(Summary = "Request for OTP", Description = "User enters mobile number and receives an OTP via SMS")]
        [SwaggerResponse(200, "OTP Sent Successfully")]
        [SwaggerResponse(400, "Invalid Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [HttpPost("request-otp")]
        public async Task<IActionResult> RequestOTP([FromBody] RequestOTPDTO requestOTPDTO)
        {
            var validationResult = await _requestOTPValidator.ValidateAsync(requestOTPDTO);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                return BadRequest(new ResponseData
                {
                    Code = Convert.ToInt16(HttpStatusCode.BadRequest),
                    Message = _localizer[name: ResponseMessage.Fail.ToString()].Value,
                    Status = false,
                    Data = errors
                });
            }

            var data = await _authService.RequestOTP(requestOTPDTO);
            return Ok(new ResponseData
            {
                Code = Convert.ToInt16(HttpStatusCode.OK),
                Message = _localizer[name: ResponseMessage.Success.ToString()].Value,
                Status = true,
                Data = data
            });

        }


        [SwaggerOperation(Summary = "Login via Mobile With OTP", Description = "User enter mobile no with OTP")]
        [SwaggerResponse(200, "OTP Login Successfully")]
        [SwaggerResponse(400, "Invalid Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOTP([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var validationResult = await _loginValidator.ValidateAsync(loginRequestDTO);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                return BadRequest(new ResponseData
                {
                    Code = Convert.ToInt16(HttpStatusCode.BadRequest),
                    Message = _localizer[name: ResponseMessage.Fail.ToString()].Value,
                    Status = false,
                    Data = errors
                });
            }

            var data = await _authService.VerifyOTP(loginRequestDTO);
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
