using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection 
    {
        public static IServiceCollection AddInfrastructureServices
            (this IServiceCollection services, IConfiguration configuration) =>
            services
            .AddDatabase(configuration);

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("Database");
            //Add Service to the container
            //services.AddDbContext<ApiDbContext>(options =>
            //{
            //    options.UseSqlServer(connectionString);
            //});
            //services.AddScoped<IApiDbContext>(sp => sp.GetRequiredService<ApiDbContext>());
            return services;
        }
    }
}
