using Constracts.Domains.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Persistance
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {

        }
        public DbSet<Entities.Customer> Customer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Entities.Customer>().HasIndex(x => x.UserName)
                .IsUnique();
            modelBuilder.Entity<Entities.Customer>().HasIndex(x => x.EmailAddress)
                .IsUnique();
        }
    }
}
