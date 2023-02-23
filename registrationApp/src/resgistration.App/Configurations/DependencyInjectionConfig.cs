using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using registration.Business.Interfaces;
using registration.Data.Repository;
using resgistration.App.Data;
using resgistration.App.Extensions;
using System.Globalization;

namespace resgistration.App.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {

            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddSingleton<IValidationAttributeAdapterProvider, CoinValidationAttributeAdapterProvider>();

            return services;
        }
    }

}
