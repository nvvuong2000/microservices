using Constracts.Domains;
using EventBus.Message.IntegrationEvent.Interface;
using MassTransit;
using Ordering.API.Application.IntegrationsEvents.EventHandler;
using Ordering.Domain.Configurations;
using Shared.Configuration;

namespace Ordering.API.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddConfigurationEmailSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var emailSettings = configuration.GetSection(nameof(SMTPEmailSetting))
                .Get<SMTPEmailSetting>();
            services.AddSingleton(emailSettings);
            services.AddScoped<IEmailSettings, SMTPEmailSetting>();

            return services;
        }

        public static void AddAppConfigurations2(this ConfigureWebHostBuilder host)
        {
            host.ConfigureAppConfiguration((context, config) =>
            {
                var env = context.HostingEnvironment;
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            });
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
                config.AddConsumersFromNamespaceContaining<BasketCheckoutEventHandler>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(mqConnection);
                    //cfg.ReceiveEndpoint("basket-checkout-queue", c =>
                    //{
                    //    c.ConfigureConsumer<BasketCheckoutEventHandler>(ctx);
                          
                    //});

                    // bat ki thang trien khai interface Iconsumer thi rabbitmq chay het
                    cfg.ConfigureEndpoints(ctx);
                });
                
                // 
                //config.AddRequestClient<IBasketCheckoutEvent>();
            });
        }
    }
}
