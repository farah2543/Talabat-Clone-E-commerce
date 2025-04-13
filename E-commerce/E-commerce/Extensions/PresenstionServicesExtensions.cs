using E_commerce.Factories;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Extensions
{
    public static class PresentationServiceExtensions
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            // Add controllers from the assembly containing Presentation.AssemblyReference
            services.AddControllers()
                    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

            // Add API Explorer for endpoint metadata
            services.AddEndpointsApiExplorer();

            // Add Swagger generation
            services.AddSwaggerGen();

            // Configure custom model state validation response
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse;
            });

            return services;

        }

    }

    
}
