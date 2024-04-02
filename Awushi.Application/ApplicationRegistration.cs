using Awushi.Application.Common;
using Awushi.Application.Services;
using Awushi.Application.Services.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awushi.Application
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped(typeof(IPaginationService<,>), typeof(PaginationService<,>));

            services.AddScoped<IAuthService,AuthService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBrandService,BrandService>();
            services.AddScoped<IProductService, ProductService>();
            //services.AddScoped<IS3Service,S3Service>();

            return services;
        }
    }
}
