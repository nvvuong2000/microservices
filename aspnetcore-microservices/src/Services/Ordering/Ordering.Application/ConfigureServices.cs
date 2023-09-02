using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ordering.Application.Common.Features.Query;
using Shared.SeedWork;
using Ordering.Application.Common.Model;
using Ordering.Application.Common.Features.Commands;

namespace Ordering.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices2(this IServiceCollection services)
        {
            // services.AddAutoMapper(Assembly.GetExecutingAssembly());
            // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //services.AddOptions();
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<IRequestHandler<GetOrdersQuery, ApiResult<List<OrderDto>>>, GetOrdersQueryHandler>();
            services.AddTransient<IRequestHandler<CreateOrderCommand, ApiResult<long>>, CreateOrUpdateOrderCommandHandler>();
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavious<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
