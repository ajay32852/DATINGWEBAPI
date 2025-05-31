using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Ocsp;
using DATINGWEBAPI.BAL.Utilities.Extensions;
using DATINGWEBAPI.BLL.Utilities.CustomExceptions;
using Serilog.Context;
using System.Net;

namespace DATINGWEBAPI.BAL.Utilities.CustomExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHttpContextAccessor httpContextAccessor)
        {
            _next = next;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }

            catch (UserNotFoundException ex)
            {
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.BadRequest);
            }
            catch (UserBlockedException ex)
            {
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.Forbidden);
            }
            catch (UserPasswordIncorrectException ex)
            {
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.Forbidden, ex.LoginCount);
            }
            catch (UserPasswordExpiredException ex)
            {
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.Forbidden);
            }
            catch (UserOTPInvalidException ex)
            {
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.BadRequest);
            }
            catch (UserOTPExpiredException ex)
            {
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.BadRequest);
            }
            catch (UserSecurityQuestionNotSet ex)
            {
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError);
            }
            catch (UserDuplicateException ex)
            {
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError);
            }
            catch (IsFirstLoginException ex)
            {
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.ResetContent);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError);
            }

        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode httpStatusCode)
        {
            var clientIp = context.Connection.RemoteIpAddress?.ToString();
            var userId = _httpContextAccessor.HttpContext.User.GetLoggedInUserId<string>();
            var userEmail = _httpContextAccessor.HttpContext.User.GetLoggedInUserName();
            var eventDateTime = DateTimeOffset.Now;
            using (LogContext.PushProperty("IPADDRESS", clientIp))
            using (LogContext.PushProperty("USERID", userId))
            using (LogContext.PushProperty("USEREMAIL", userEmail))
            using (LogContext.PushProperty("EVENTDATETIME", eventDateTime))

                _logger.LogError(exception.Message + "-" + exception.StackTrace, clientIp, userId, userEmail, eventDateTime);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatusCode;

            await context.Response.WriteAsync(new ResponseData()
            {
                Code = context.Response.StatusCode,
                Message = exception.Message,
                Status = false,
                Data = null
            }.ToString());
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode httpStatusCode, int loginAttempt)
        {
            var clientIp = context.Connection.RemoteIpAddress?.ToString();
            var userId = _httpContextAccessor.HttpContext.User.GetLoggedInUserId<string>();
            var userEmail = _httpContextAccessor.HttpContext.User.GetLoggedInUserName();
            var eventDateTime = DateTimeOffset.Now;
            using (LogContext.PushProperty("IPADDRESS", clientIp))
            using (LogContext.PushProperty("USERID", userId))
            using (LogContext.PushProperty("USEREMAIL", userEmail))
            using (LogContext.PushProperty("EVENTDATETIME", eventDateTime))

                _logger.LogError(exception.Message + "-" + exception.StackTrace, clientIp, userId, userEmail, eventDateTime);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatusCode;

            await context.Response.WriteAsync(new ResponseData()
            {
                Code = context.Response.StatusCode,
                Message = exception.Message,
                Status = false,
                Data = loginAttempt
            }.ToString());
        }
    }
}
