using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Common.Behaviours;
using Ordering.Application.Common.Features.V1.Queries.GetOrders;
using Ordering.Application.Common.Model;
using Shared.SeedWork;
using System.Reflection;

namespace Ordering.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddOptions();
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            // services.AddTransient<IRequestHandler<GetOrdersQuery, ApiResult<List<OrderDto>>>, GetOrdersQueryHandler>();
          //  services.AddTransient(typeof(IRequestHandler<,>), typeof(GetOrdersQueryHandler<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavious<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}