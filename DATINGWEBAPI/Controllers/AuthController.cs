using DATINGWEBAPI.BAL.Utilities.Common;
using DATINGWEBAPI.BLL.Utilities.CustomExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Org.BouncyCastle.Asn1.Ocsp;
using DATINGWEBAPI.BAL.Services.IServices;
using DATINGWEBAPI.DTO.RequestDTO;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using FluentValidation;
using DATINGWEBAPI.BAL.Services;

namespace DATINGWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IValidator<LoginRequestDTO> _loginValidator;
        private readonly ILogger<AuthController> _logger;
        private readonly IStringLocalizer<ResponseMessage> _localizer;
        private readonly IAuthService _authService;

        public AuthController(
            IValidator<LoginRequestDTO> loginValidator,
            ILogger<AuthController> logger,
            IStringLocalizer<ResponseMessage> localizer,
            IAuthService authService)
        {
            _loginValidator = loginValidator;
            _logger = logger;
            _localizer = localizer;
            _authService = authService;
        }

        /// <summary>
        /// login with OTP
        /// </summary>
        /// <param name="userLoginDto"></param>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Login via Mobile", Description = "User enters mobile number and receives an OTP via SMS")]
        [SwaggerResponse(200, "OTP Sent Successfully")]
        [SwaggerResponse(400, "Invalid Request")]
        [SwaggerResponse(401, "Unauthorized")]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO userLoginDto)
        {
            var validationResult = await _loginValidator.ValidateAsync(userLoginDto);
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

            var data = await _authService.Login(userLoginDto);
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
