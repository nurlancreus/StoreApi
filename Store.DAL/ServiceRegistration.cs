using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using Store.Core.Abstractions.Repositories;
using Store.DAL.Context;
using Store.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL
{
    public static class ServiceRegistration
    {
        public static void AddDALServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Adding db context
            services.AddDbContext<StoreApiDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));

            // Adding Category repositories
            services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
            
            // Adding Product repositories
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        }
    }
}
