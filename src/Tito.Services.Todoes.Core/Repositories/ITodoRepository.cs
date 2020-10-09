using System;
using System.Linq;
using System.Threading.Tasks;
using Tito.Services.Todoes.Core.Entities;

namespace Tito.Services.Todoes.Core.Repositories
{
    public interface ITodoRepository
    {
        IQueryable<Todo> GetAllAsync();

        Task<Todo> GetAsync(Guid id);

        Task AddAsync(Todo todo);

        Task UpdateAsync(Todo todo);

        Task DeleteAsync(Todo todo);
    }
}
