using Constracts.Common.Interfaces;
using Constracts.Message;
using Constracts.Services;
using Infrastructure.Common;
using Infrastructure.Message;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Common.Interfaces;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repository;
using Ordering.Infrastructure.Services;
using Shared.Services.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var abc = configuration.GetConnectionString("DefaultConnectionString");
            services.AddDbContext<OrderContext>((options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"));
            }));

            services.AddScoped(typeof(IRepositoryBaseAsync<,,>), typeof(RepositoryBaseAsync<,,>));
            services.AddScoped<OrderContextSeed>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped(typeof(IEmailSevices<MailRequest>), typeof(SmtpEmailService));
            services.AddScoped<IMessageProducer, RabbitMQProducer>();
            //   services.AddTransient(typeof(ISmtpEMailServices), typeof(SmtpEmailService));
            //    IEmailSettings

            return services;
        }
    }
}
