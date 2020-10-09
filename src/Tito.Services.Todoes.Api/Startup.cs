using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Tito.Services.Todoes.Application;
using Tito.Services.Todoes.Infrastructure;
using Tito.Services.Todoes.Infrastructure.Data;

namespace Tito.Services.Todoes.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddApplication();
            services.AddScoped<ErrorHandlerMiddleware>();
            services.Configure<ApiOptions>(_configuration.GetSection("api"));
            services.AddEntityFramework<TodoContext>();
            services.AddSwaggerGen(swagger=> {
                swagger.SwaggerDoc("Todo", new OpenApiInfo
                {
                    Title = "Todo Api",
                    Version = "V1"
                });
            });
            
            ConfigureTestServices();

        }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<TodoContext>();
                context.Database.EnsureCreated();
            }
            app.UseSwagger();
            app.UseSwaggerUI(swagger =>
            {
                swagger.SwaggerEndpoint("/swagger/Todo/swagger.json", "Todo Api v1");
            });
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        protected virtual void ConfigureTestServices()
        {

        }
    }
}
