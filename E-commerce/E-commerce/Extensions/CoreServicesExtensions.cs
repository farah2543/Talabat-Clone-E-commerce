using Services.Abstraction;
using Services;
using Shared;

namespace E_commerce.Extensions
{
    public static class CoreServicesExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services,IConfiguration configuration)
        {
            // Register ServiceManager as the implementation of IServiceManager with scoped lifetime
            services.AddScoped<IServiceManager, ServiceManager>();
            
            // Add AutoMapper and scan the assembly containing Services.AssemblyReference for mapping profiles
            services.AddAutoMapper(typeof(Services.AssemblyReference).Assembly);

            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

            return services;
        }


    }
}
