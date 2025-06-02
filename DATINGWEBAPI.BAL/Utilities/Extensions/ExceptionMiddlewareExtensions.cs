using DATINGWEBAPI.BAL.Utilities.CustomExceptionMiddleware;
using Microsoft.AspNetCore.Builder;
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
