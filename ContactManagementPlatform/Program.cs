using ContactManagementPlatform.Filters.ActionFilters;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoryContracts;
using Serilog;
using ServiceContracts;
using Services;
using System.Configuration;

namespace ContactManagementPlatform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Serilog
            builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider service, LoggerConfiguration loggerConfiguration) =>
            {
                loggerConfiguration.ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(service);
            });

            builder.Services.ConfigureSrevise(builder.Configuration);

            if (builder.Environment.IsEnvironment("Test") == false)
            {
                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                });
            }

            var app = builder.Build();

            if (builder.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (builder.Environment.IsEnvironment("Test") == false)
            {
                Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");
            }

            app.UseSerilogRequestLogging();
            app.UseHttpLogging();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();

            app.Run();
        }
    }
}
