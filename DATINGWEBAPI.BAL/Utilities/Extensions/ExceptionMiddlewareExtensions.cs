using DATINGWEBAPI.BAL.Utilities.CustomExceptionMiddleware;
using Microsoft.AspNetCore.Builder;
using DATINGWEBAPI.BAL.Utilities.CustomExceptionMiddleware;
namespace DATINGWEBAPI.BAL.Utilities.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {

        public static void ConfigureCustomExceptionMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
