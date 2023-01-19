
using Basket.API.Repository.Interface;

namespace Basket.API.Extensions
{
    public static class ServicesExtensions
    {
        private static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            return services.AddScoped<IBasketRepository, IBasketRepository>();
        }
    }
}
