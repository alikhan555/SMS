using Application.Common.Interfaces;
using Domain.Entities.User;
using Infrastructure.Identity.Services;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            // DbContext Configuration
            services.AddDbContext<SMSDbContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<ISMSDbContext>(provider => provider.GetRequiredService<SMSDbContext>());
            
            services.AddScoped<IDateTime, DateTimeService>();
            services.AddScoped<IUserManager, UserManagerService>();

            // Identity Configuration
            services.AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddRoles<AppRole>()
            .AddEntityFrameworkStores<SMSDbContext>()
            .AddSignInManager<SignInManager<AppUser>>();
            services.AddAuthentication();
            services.AddScoped<TokenService>();

            return services;
        }
    }
}
