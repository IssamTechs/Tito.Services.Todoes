using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Tito.Services.Todoes.Core.Repositories;
using Tito.Services.Todoes.Infrastructure.Repositories;

namespace Tito.Services.Todoes.Infrastructure
{
    public static class Extensions
    { 
        public static IServiceCollection AddEntityFramework<T>(this IServiceCollection services)
            where T : DbContext
        {
            services.AddScoped<ITodoRepository, TodoRepository>();
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>(); 
            var model = new SqlOption();
            configuration.GetSection("sql").Bind(model);
            services.AddDbContext<T>(options => {
                options.UseSqlServer(model.ConnectionString);
            });

            return services;
        }
    }
}
