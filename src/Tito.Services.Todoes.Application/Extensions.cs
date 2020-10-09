using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Tito.Services.Todoes.Application.DTO;
using Tito.Services.Todoes.Application.Services;
using Tito.Services.Todoes.Core.Entities;

namespace Tito.Services.Todoes.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<TodoService, TodoService>();
            return services; 
        }

        public static TodoDto AsDto(this Todo todo)
            => new TodoDto
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                priority = todo.Priority.ToString(),
                State = todo.State.ToString(),
                CreatedAt = todo.CreatedAt
            }; 
    }
}
