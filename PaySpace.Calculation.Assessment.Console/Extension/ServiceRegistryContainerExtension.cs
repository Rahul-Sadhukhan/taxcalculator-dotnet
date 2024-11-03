using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaySpace.Calculation.Assessment.Console.Data;
using PaySpace.Calculation.Assessment.Console.Data.Repositories;
using PaySpace.Calculation.Assessment.Console.Factory;
using PaySpace.Calculation.Assessment.Console.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.Calculation.Assessment.Console.Extension
{
    public static class ServiceRegistryContainerExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<PaySpaceContext>(option => option.UseSqlServer(config.GetConnectionString("DBConnection")));
            services.AddScoped<IPaySpaceContext, PaySpaceContext>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ITaxRegimeRepository, TaxRegimeRepository>();
            services.AddScoped<ITaxCalculationRepository, ProgTaxCalculationRepository>();
            services.AddScoped<ITaxCalculationRepository, FlatTaxCalculationRepository>();
            services.AddScoped<ITaxCalculationRepository, PercTaxCalculationRepository>();
            services.AddScoped<ITaxCalculationFactory, TaxCalculationFactory>();
            services.AddScoped<IPaySpaceUnitofWork, PaySpaceUnitofWork>();
            services.AddScoped<ICalculateTaxService, CalculateTaxService>();
            services.AddScoped<Func<IPaySpaceContext, ICountryRepository>>(provider =>
            {
                return context => provider.GetRequiredService<ICountryRepository>();
            });
            services.AddScoped<Func<IPaySpaceContext, ITaxRegimeRepository>>(provider =>
            {
                return context => provider.GetRequiredService<ITaxRegimeRepository>();
            });
            services.AddScoped<Func<IPaySpaceContext, ITaxCalculationRepository>>(provider =>
            {
                return context => provider.GetRequiredService<ITaxCalculationRepository>();
            });
            services.AddScoped<Func<ITaxCalculationRepository, ITaxCalculationFactory>>(provider =>
            {
                return context => provider.GetRequiredService<ITaxCalculationFactory>();
            });
            return services;
        }
    }
}
