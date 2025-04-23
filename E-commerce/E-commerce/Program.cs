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

            builder.Services.AddCoreServices(builder.Configuration);

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

            app.UseStaticFiles();
            app.UseCors("CORSPolicy");

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();


            app.Run();

            #endregion


        }
    }
}
