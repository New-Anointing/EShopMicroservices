using BuildingBlocks.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using BuildingBlocks.Messaging.MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;

namespace Ordering.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();
            //Registering Mediatr
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(assembly);
                options.AddOpenBehavior(typeof(ValidationBehaviors<,>));
                options.AddOpenBehavior(typeof(ValidationBehaviors<,>));
            });

            //registering rabbitmq 
            services.AddMessageBroker(configuration, assembly: assembly);

            //registering feauture management
            services.AddFeatureManagement();

            return services;
        }
    }
}
