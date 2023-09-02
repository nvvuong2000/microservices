
using Basket.API.Repository.Interface;
using EventBus.Message.IntegrationEvent.Interface;
using MassTransit;
using Ordering.Application.Common.Features.Commands;
using Shared.Configuration;

namespace Basket.API.Extensions
{
    public static class ServicesExtensions
    {
        //public static IServiceCollection AddConfigurationEventBusSettings(this IServiceCollection services, IConfiguration configuration)
        //{
        //    var eventBusSettings = configuration.GetSection(nameof(EventBusSettings))
        //        .Get<EventBusSettings>();
        //    services.AddSingleton(eventBusSettings);
        //    services.AddScoped<EventBusSettings, EventBusSettings>();

        //    return services;
        //}
        private static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            return services.AddScoped<IBasketRepository, IBasketRepository>();
        }
        public static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection(nameof(EventBusSettings))
              .Get<EventBusSettings>();
            services.AddSingleton(settings);
            services.AddScoped<EventBusSettings, EventBusSettings>();
            if (settings == null || string.IsNullOrEmpty(settings.HostAddress))
                throw new ArgumentNullException("EventBusSettings is not configure");

            var mqConnection = new Uri(settings.HostAddress);
            services.AddSingleton(KebabCaseEndpointNameFormatter.Instance);
            services.AddMassTransit(config =>
            {
                config.AddConsumersFromNamespaceContaining<CreateOrUpdateOrderCommandHandler>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(mqConnection);
                });
                // 
                config.AddRequestClient<IBasketCheckoutEvent>();
            });
        }
    }
}
