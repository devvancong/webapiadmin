using Microsoft.EntityFrameworkCore;
using WebEntryModel;
using WebHelper;
using WebRepository.Implement;
using WebRepository.Interface;
using WebService.Implement;
using WebService.Interface;

namespace Webapi.Configuration
{
    public static class Configurationinstall
    {
        public static IServiceCollection GetDbContext(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddDbContext<AppDatabase>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("QLBHDatabase"));
            });

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            return services;
        }
        public static IServiceCollection GetRepository(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
        public static IServiceCollection GetServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();

            return services;
        }
    }
}