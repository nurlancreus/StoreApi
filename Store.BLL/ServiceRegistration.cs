using Amazon.S3;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.BLL.Services;
using Store.BLL.Services.Storage.AWS;
using Store.BLL.Services.Storage.Local;
using Store.Core.Abstractions.Services;
using Store.Core.Abstractions.Services.Storage;
using Store.Core.Abstractions.Services.Storage.Local;
using Store.Core.Enums;
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

        public static void AddStorage(this IServiceCollection services, StorageType storageType, IConfiguration configuration)
        {
            switch (storageType)
            {
                case StorageType.Local:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;

                case StorageType.AWS:
                    ConfigureAWSServices(services, configuration);
                    break;

                default:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
            }
        }

        private static void ConfigureAWSServices(IServiceCollection services, IConfiguration configuration)
        {
            var awsOptions = configuration.GetAWSOptions();
            awsOptions.Credentials = new Amazon.Runtime.BasicAWSCredentials(
                configuration["Storage:AWS:AccessKey"],
                configuration["Storage:AWS:SecretAccessKey"]);
            awsOptions.Region = Amazon.RegionEndpoint.GetBySystemName(configuration["Storage:AWS:Region"]); // Ensure the region is set


            services.AddDefaultAWSOptions(awsOptions);
            services.AddAWSService<IAmazonS3>();

            services.AddScoped<IStorage, AWSStorage>();
        }
    }
}
