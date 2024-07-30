using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Sample.Infrastructure.Domain.Instruments;
using Mc2.CrudTest.Domain.Customers;

namespace Mc2.CrudTest.Persistence.EntityFramework.Persistence
{
    public class SampleDbContext : DbContext
    {
        public SampleDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CustomerEntityTypeConfiguration());
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

        public DbSet<Customer> Customers { get; set; }

    }
}
