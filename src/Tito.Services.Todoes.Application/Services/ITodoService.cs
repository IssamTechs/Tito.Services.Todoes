using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tito.Services.Todoes.Application.Commands;
using Tito.Services.Todoes.Application.DTO;
using Tito.Services.Todoes.Application.Paginations;
using Tito.Services.Todoes.Application.Queries;

namespace Tito.Services.Todoes.Application.Services
{
    public interface ITodoService
    {
        Task AddAsync(AddTodo command);
        Task DeleteAsync(DeleteTodo command);
        Task<TodoDto> GetAsync(Guid id);
        Task<PagedResult<TodoDto>> SearchAsync(SearchTodo query);
        Task UpdateAsync(UpdateTodo command);
    }
}