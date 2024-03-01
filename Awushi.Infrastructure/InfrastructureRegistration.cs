using Awushi.Domain.Contracts;
using Awushi.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awushi.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfrastructureServices (this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof (GenericRepository<>));
            services.AddScoped<ICategoryRepostitory, CategoryRepostitory>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            return services;
        }
    }
}
