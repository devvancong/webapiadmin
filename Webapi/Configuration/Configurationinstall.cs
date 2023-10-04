using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Webapi.Authenticate;
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

            services.AddTransient<IAuthenticate, Authenticates>();
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
        public static IServiceCollection Authentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var Key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };
            });
            services.AddAuthorization();

            return services;
        }
    }
}