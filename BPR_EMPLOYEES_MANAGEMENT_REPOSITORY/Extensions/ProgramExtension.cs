using BPR_EMPLOYEES_MANAGEMENT_CORE.Interface.Configuration;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using BPR_EMPLOYEES_MANAGEMENT_INFRASTRUCTURE.Services;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Interface.Models;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Service;
using BPR_EMPLOYEES_MANAGEMENT_INFRASTRUCTURE.Repositories;
using BPR_EMPLOYEES_MANAGEMENT_CORE.Interface.ConnectionDB;

namespace BPR_EMPLOYEES_MANAGEMENT_INFRASTRUCTURE.Extensions
{
    public static class ProgramExtension
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(options =>
            {
            });

            services.AddMvcCore().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = (errorContext) =>
                {
                    var errors = errorContext.ModelState.Values.SelectMany(e => e.Errors.Select(m => new { m.ErrorMessage })).ToList();

                    var result = new
                    {
                        ResponseCode = "1",
                        DescriptionResponseCode = "Error en validación de campos, " + string.Join("|", errors.Select(e => e.ErrorMessage).ToList())
                    };

                    return new Microsoft.AspNetCore.Mvc.OkObjectResult(result);
                };
            });
            services.AddAuthorization();
            services.Configure<ConfigurationLog>(configuration.GetSection(BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Configuration.ConfigurationSection.LogService));
            //services.Configure<ConfigurationMessages>(configuration.GetSection(CORE.Model.Configuration.ConfigurationSection.ConfigurationMessages));
            services.Configure<ConfigurationDB>(configuration.GetSection("DbModel"));
            services.Configure<ConfigurationDB>(configuration.GetSection("ConnectionStrings"));
            services.ConfigureWritable<ConfigurationDB>(configuration.GetSection(BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Configuration.ConfigurationSection.ConnectionStrings));
            services.ConfigureWritable<ConfigurationWeb>(configuration.GetSection(BPR_EMPLOYEES_MANAGEMENT_CORE.Model.Configuration.ConfigurationSection.WebConfiguration));

            /*Configurations*/
            services.AddTransient<IConfigurationService, ConfigurationService>();
            services.AddTransient<ILogService, LogService>();
            services.AddTransient<IDbService, DbService>();
            services.AddTransient<IParseService, ParseService>();
            services.AddTransient<IHttpService, HttpService>();
            services.AddTransient<IEmployeesService, EmployeesService>();
            services.AddTransient<IEmployeesRepository, EmployeesRepository>();
            services.AddTransient<IEmployeesCallSPRepository,EmployeesCallSPRepository>();
           

        }

        public static void ConfigureWritable<T>(
            this IServiceCollection services,
            IConfigurationSection section,
            string file = "appsettings.json") where T : class, new()
        {
            services.Configure<T>(section);
            services.AddTransient<IWritableOptionsService<T>>(provider =>
            {
                var configuration = (IConfigurationRoot)provider.GetService<IConfiguration>();
                var environment = provider.GetService<IHostEnvironment>();
                var options = provider.GetService<IOptionsMonitor<T>>();
                return new WritableOptionsService<T>(environment, options, configuration, section.Key, file);
            });
        }


        public static void AddUses(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
