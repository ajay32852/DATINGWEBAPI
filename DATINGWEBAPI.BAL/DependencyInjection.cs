using CloudinaryDotNet;
using DATINGWEBAPI.BAL.Services;
using DATINGWEBAPI.BAL.Services.IServices;
using DATINGWEBAPI.BAL.Utilities.AutoMapperProfiles;
using DATINGWEBAPI.BAL.Validators;
using DATINGWEBAPI.DTO.DTOs;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
namespace DATINGWEBAPI.BAL
{
    public static class DependencyInjection
    {
        public static void RegisterBLLDependencies(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
            services.AddSingleton(serviceProvider =>
            {
                var config = serviceProvider.GetRequiredService<IOptions<CloudinarySettings>>().Value;
                Account account = new(config.CloudName, config.ApiKey, config.ApiSecret);
                return new Cloudinary(account);
            });
            // ADD SIGNALR SERVICES
            services.AddSignalR();
            services.AddAutoMapper(typeof(AutoMapperProfiles));
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<UserToLoginDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<LoginRequestDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateUserProfileRequestValidator>();
            services.AddFluentValidationClientsideAdapters();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ISwipeService, SwipeService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<INotificationHubClient, NotificationHubClient>();
        }

    }
}
