using Domain.Contracts;
using E_commerce.Middlewares;

namespace E_commerce.Extensions
{
    public static class WebAppExtensions
    {
        public static async Task<WebApplication> SeedDbAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
                await dbInitializer.InitializeAsync();
                await dbInitializer.InitializeIdentityAsync();
                     
                return app;
            }
        }

        public static WebApplication UseCustomMiddleWare(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            return app;

        }
    }
}
