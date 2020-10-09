using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tito.Services.Todoes.Core.Entities;
using Tito.Services.Todoes.Core.Repositories;
using Tito.Services.Todoes.Infrastructure.Data;

namespace Tito.Services.Todoes.Infrastructure.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;

        public TodoRepository(TodoContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Todo todo)
        {
            _context.Add(todo);
             await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Todo todo)
        {
            _context.Remove(todo);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Todo> GetAllAsync()
        { 
            return _context.Todoes;
        }

        public async Task<Todo> GetAsync(Guid id)
        {
            return await _context.Todoes.FindAsync(id);
        }

        public async Task UpdateAsync(Todo todo)
        {
            _context.Update(todo);
            await _context.SaveChangesAsync();
        }
    }
}
