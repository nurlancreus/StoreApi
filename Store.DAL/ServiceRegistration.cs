using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using Store.DAL.Context;
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
            //services.AddDbContext<MiniECommerceDbContext>(options => options.UseNpgsql(Configuration.ConnectionString));
            services.AddDbContext<StoreApiDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));
        }
    }
}
