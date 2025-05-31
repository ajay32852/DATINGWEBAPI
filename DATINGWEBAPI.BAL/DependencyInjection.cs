using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DATINGWEBAPI.BAL.Services;
using DATINGWEBAPI.BAL.Services.IServices;
using DATINGWEBAPI.BAL.Utilities.AutoMapperProfiles;
using DATINGWEBAPI.BAL.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace DATINGWEBAPI.BAL
{
    public static class DependencyInjection
    {
        public static void RegisterBLLDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddAutoMapper(typeof(AutoMapperProfiles));
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<UserToLoginDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<LoginRequestDTOValidator>();
            services.AddFluentValidationClientsideAdapters();

            //services.AddScoped<IUserService, UserService>();
             services.AddScoped<IAuthService, AuthService>();

            /*services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IParkingLotsService, ParkingLotsService>();
            services.AddScoped<ICategoryService, CategoryService>();*/


        }

    }
}
