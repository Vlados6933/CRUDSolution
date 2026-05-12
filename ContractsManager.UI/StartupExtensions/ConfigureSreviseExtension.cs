

using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.ServiceContracs;
using ContactsManager.Core.Services;
using ContactsManager.Infrastructure.Repositories;
using ContactsManager.UI.Filters.ActionFilters;

namespace ContactsManager.UI.StartupExtensions
{
    public static class ConfigureSreviseExtension
    {
        public static IServiceCollection ConfigureSrevise(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews(options =>
            {
                var logger = services.BuildServiceProvider().
                GetRequiredService<ILogger<ResponseHeaderActionFilter>>();

                options.Filters.Add(new ResponseHeaderActionFilter(logger)
                {
                    Key = "MyKey-From-Global",
                    Value = "MyValue-From-Global",
                    Order = 2
                });
            });

            services.AddTransient<ResponseHeaderActionFilter>();
            services.AddScoped<ICountriesService, CountriesService>();
            services.AddScoped<IPersonsService, PersonsService>();
            services.AddScoped<ICountriesRepository, CountriesRepository>();
            services.AddScoped<IPersonsRepository, PersonsRepository>();
            services.AddHttpLogging();
            
            return services;
        }
    }
}
