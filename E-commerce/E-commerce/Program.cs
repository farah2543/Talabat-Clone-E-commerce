
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using persistence.Repositories;
using Persistence.Data;
using Persistence.Data.DataSeeding;
using Services.Abstraction;
using Services;
using System.Net.WebSockets;
using System.Reflection.Metadata;
using E_commerce.Middlewares;
using E_commerce.Factories;
using Microsoft.AspNetCore.Mvc;
using E_commerce.Extensions;

namespace E_commerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Services

            builder.Services.AddPresentationServices();

            builder.Services.AddCoreServices();

            builder.Services.AddInfrastructureServices(builder.Configuration);
            #endregion

            #region MiddleWare

            var app = builder.Build();
            app.UseCustomMiddleWare();

            await app.SeedDbAsync();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseStaticFiles();


            app.MapControllers();


            app.Run();

            #endregion


        }
    }
}
