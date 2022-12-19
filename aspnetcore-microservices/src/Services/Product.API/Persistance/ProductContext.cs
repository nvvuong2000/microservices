using Constracts.Domains.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Product.API.Persistance
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }
        public DbSet<Entities.Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Entities.Product>().HasIndex(x => x.No)
                .IsUnique();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var modifield = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified ||
                       e.State == EntityState.Deleted ||
                       e.State == EntityState.Added);
            foreach (var item in modifield)
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        if (item.Entity is IDateTracking addedEntity)
                        {
                            addedEntity.CreateDate = DateTime.UtcNow;
                            item.State = EntityState.Added;
                        }
                        break;

                    case EntityState.Modified:
                        Entry(item.Entity).Property("Id").IsModified = false;
                        if(item.Entity is IDateTracking modifieldEntity)
                        {
                            modifieldEntity.LastModifiedDate = DateTime.UtcNow;
                            item.State = EntityState.Modified;
                        }
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
