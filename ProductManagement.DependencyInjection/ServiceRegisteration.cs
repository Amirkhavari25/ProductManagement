using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Application.Contracts.Interfaces;
using ProductManagement.Application.Contracts.Persistence;
using ProductManagement.Infrastructure.Persistence.Repositories;
using ProductManagement.Infrastructure.Persistence;
using ProductManagement.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ProductManagement.DependencyInjection
{
    public static class ServiceRegisteration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            //JWT Setting
            var JWTSetting = configuration.GetSection("JWTSetting");
            var SecretKey = Encoding.UTF8.GetBytes(JWTSetting["TokenSecret"]);
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false; // for developement enviroment no need to use exactly HTTPS
                opt.SaveToken = true;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(SecretKey),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = JWTSetting["Issuer"],
                    ValidAudience = JWTSetting["Audience"]
                };
            });



            //Identity dependencies
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;

                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._";
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDBContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
