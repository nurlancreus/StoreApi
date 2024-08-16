using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.BLL.Services;
using Store.Core.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL
{
    public static class ServiceRegistration
    {
        public static void AddBLLServices(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            services.AddAutoMapper(typeof(ServiceRegistration).Assembly);

            services.AddScoped<ICategoryService, CategoryService>();
        }
    }
}
