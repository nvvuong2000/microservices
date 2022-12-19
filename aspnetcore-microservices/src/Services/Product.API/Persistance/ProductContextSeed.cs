using ILogger = Serilog.ILogger;
using Product.API.Entities;
namespace Product.API.Persistance
{
    public class ProductContextSeed
    {
        public static async Task SeedProductAsync(ProductContext productContext, ILogger logger)
        {
            if (!productContext.Products.Any())
            {
                productContext.AddRange(getListProducts());
                await productContext.SaveChangesAsync();
                logger.Information("Seeded data for Product DB with context {DBContextName}", nameof(ProductContext));
            }
        }

        private static IEnumerable<Entities.Product> getListProducts()
        {
            return new List<Entities.Product>
            {
                new()
                {
                    No = "Lotus",
                    Name= "Esprit",
                    Summary="Lorem isum",
                    Description="Lorem isum",
                    Price = (decimal)7.678,
                },
                new()
                {
                    No = "Lotus 1",
                    Name= "Esprit 2",
                    Summary="Lorem isum 2",
                    Description="Lorem isum 2",
                    Price = (decimal)987.98,
                },

            };
        }
    }
}
