using Demo.API.Common.Extensions;
using Demo.API.Common.Logging;
using Demo.API.Respository;
using Demo.API.Respository.Factory;
using Demo.API.Respository.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace Demo.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public ILoggingService LoggingService { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.AddMvcCore(new List<JsonConverter>() { new StringEnumConverter() });
            services.AddHeaderVersioning();
            services.AddSwagger();
            LoggingService = new SerilogLoggingService(Configuration);
            services.AddSingleton(_ => LoggingService);
            services.AddHttpClient();


            services.AddScoped<Managers.IPatientManager, Managers.PatientManager>();
            services.AddScoped<Managers.ILocationManager, Managers.LocationManager>();
            services.AddScoped<IRepositoryFactory, RepositoryFactory>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();


            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseCors(Configuration);
            app.UseHttpsRedirection();

            app.UseSwagger(provider);
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseLogging();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
