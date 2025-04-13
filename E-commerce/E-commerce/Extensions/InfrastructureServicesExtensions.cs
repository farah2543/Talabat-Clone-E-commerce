using Domain.Contracts;
using Persistence.Data.DataSeeding;
using Persistence.Data;
using persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Connections.Features;
using StackExchange.Redis;

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

            // Register database initializer
            services.AddScoped<IDbInitializer, DbInitializer>();

            // Register Unit of Work pattern implementation
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IConnectionMultiplexer>(services =>
            ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!));



            return services;





        }
   




    }
}
