using DATINGWEBAPI.DAL.DataContext;
using DATINGWEBAPI.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DATINGWEBAPI.DAL.Repositories;

namespace DATINGWEBAPI.DAL
{
    public static class DependencyInjection
    {
        public static void RegisterDalDependencies(this IServiceCollection services, IConfiguration configuration)
        {
           services.AddDbContext<DatingAPPContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVerificationCodeRepository, VerificationCodeRepository>();
            services.AddScoped<ISwipeRepository, SwipeRepository>();

        }

    }
}
