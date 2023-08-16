using Mc2.CrudTest.Application;
using MediatR;
using Mc2.CrudTest.Persistence.EntityFramework.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Mc2.CrudTest.Domain.Contract;

namespace Mc2.CrudTest.Presentation.Server.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            services.AddMediatR(typeof(Startup).Assembly, typeof(AssembelyRecognizer).Assembly);
            
            services.AddConnection(configuration);
            return services;
        }
    }
}
