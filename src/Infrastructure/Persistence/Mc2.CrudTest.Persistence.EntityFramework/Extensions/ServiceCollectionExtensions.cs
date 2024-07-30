using Humanizer.Configuration;
using IdGen;
using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Customers.DomainServices;
using Mc2.CrudTest.Persistence.EntityFramework.Domain.Customers;
using Mc2.CrudTest.Persistence.EntityFramework.EventProcessing;
using Mc2.CrudTest.Persistence.EntityFramework.Persistence;
using Mc2.CrudTest.Presentation.Shared.EventProcessing.DomainEvent;
using Mc2.CrudTest.Presentation.Shared.SeedWork;
using Mc2.CrudTest.Presentation.Shared.Shared;
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
            services.AddDbContext<SampleDbContext>(options => options.
               UseSqlServer(mssqlConnection));

            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ICustomerDuplicationValidatorService, CustomerDuplicationValidatorService>();
            services.AddTransient<IEmailAddressDuplicationValidatorService, EmailAddressDuplicationValidatorService>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IDomainEventsDispatcher, DomainEventsDispatcher>();

            services.AddTransient<IIdGenerator, SnowflakeIdGenerator>();

            return services;
        }

        public static IServiceCollection AddConnection(this IServiceCollection services, IConfiguration configuration)
        {
            AddRepository(services, configuration);

            return services;
        }
    }
}
