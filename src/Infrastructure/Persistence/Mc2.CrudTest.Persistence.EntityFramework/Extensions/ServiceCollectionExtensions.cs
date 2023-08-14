using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Persistence.EntityFramework.Domain.Customers;
using Mc2.CrudTest.Persistence.EntityFramework.EventProcessing;
using Mc2.CrudTest.Persistence.EntityFramework.Persistence;
using Mc2.CrudTest.Presentation.Shared.EventProcessing.DomainEvent;
using Mc2.CrudTest.Presentation.Shared.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.Persistence.EntityFramework.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            var mssqlConnection = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            services.AddDbContextPool<SampleDbContext>(options =>
            {
                options.UseSqlServer(mssqlConnection);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.EnableSensitiveDataLogging(true);
            }, 1024);

            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();

            return services;
        }

        public static IServiceCollection AddConnection(this IServiceCollection services, IConfiguration configuration)
        {
            AddRepository(services, configuration);

            return services;
        }
    }
}
