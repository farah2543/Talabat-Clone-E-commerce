using Domain.Contracts;
using Persistence.Data.DataSeeding;
using Persistence.Data;
using persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace E_commerce.Extensions
{
    public static class InfrastructureServicesExtensions
    {

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {

            // Register DbContext with SQL Server provider

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"));
            });

            services.AddDbContext<IdentityAppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnectionString"));
            });

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.User.RequireUniqueEmail = true;


            }).AddEntityFrameworkStores<IdentityAppDbContext>();

            services.AddSingleton<IConnectionMultiplexer>(services =>
            ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!));

            // Register database initializer
            services.AddScoped<IDbInitializer, DbInitializer>();

            // Register Unit of Work pattern implementation
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IBasketRepository, BasketRepository>();

            services.ConfigureJwt(configuration);


            return services;





        }

        public static IServiceCollection ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {

            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))

                };
            });

            services.AddAuthorization();

            return services;





        }

    }
}
