using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tito.Services.Todoes.Application.Commands;
using Tito.Services.Todoes.Application.DTO;
using Tito.Services.Todoes.Application.Exceptions;
using Tito.Services.Todoes.Application.Paginations;
using Tito.Services.Todoes.Application.Queries;
using Tito.Services.Todoes.Core.Entities;
using Tito.Services.Todoes.Core.Repositories;
using Tito.Services.Todoes.Core.ValueObjects;

namespace Tito.Services.Todoes.Application.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<TodoDto> GetAsync(Guid id)
        {
            var todo = await _todoRepository.GetAsync(id);
            return todo is null
                ? null
                : new TodoDto
                {
                    Id = todo.Id,
                    Title = todo.Title,
                    Description = todo.Description,
                    priority = todo.Priority.ToString().ToLowerInvariant(),
                    State = todo.State.ToString().ToLowerInvariant(),
                    CreatedAt = todo.CreatedAt
                };
        }

        public async Task AddAsync(AddTodo command)
        {
            if (!Enum.TryParse<Priority>(command.Priority, true, out var priority))
            {
                throw new InvalidTodoPriorityException(command.Priority);
            }

            var todo = new Todo(command.Id, command.Title, command.Description, priority, State.NEW, command.CreatedAt);

            await _todoRepository.AddAsync(todo);
        }

        public async Task UpdateAsync(UpdateTodo command)
        {
           

            if (!Enum.TryParse<State>(command.State, true, out var state))
            {
                throw new InvalidTodoStateException(command.State);
            }

            if (!Enum.TryParse<Priority>(command.Priority, true, out var priority))
            {
                throw new InvalidTodoPriorityException(command.Priority);
            }

            var todo = new Todo(command.Id, command.Title, command.Description, priority, state);

            await _todoRepository.UpdateAsync(todo);
        }

        public async Task DeleteAsync(DeleteTodo command)
        {
            var todo = await _todoRepository.GetAsync(command.TodoId);

            if (todo is null)
            {
                throw new TodoNotFoundException(command.TodoId);
            }

            await _todoRepository.DeleteAsync(todo);
        }

        public async Task<PagedResult<TodoDto>> SearchAsync(SearchTodo query)
        {
            var result = _todoRepository.GetAllAsync();

            if (!(query is null))
            {
                if (!string.IsNullOrWhiteSpace(query.Title))
                {
                    result = result.Where(x => x.Title.Value.Contains(query.Title));
                }
                if (!string.IsNullOrWhiteSpace(query.Description))
                {
                    result = result.Where(x => x.Description.Value.Contains(query.Description));
                }
                if (!string.IsNullOrWhiteSpace(query.Priority))
                {
                    if (Enum.TryParse<Priority>(query.Priority, true, out var priority))
                    {
                        result = result.Where(x => x.Priority == priority);
                    }
                }
                if (!string.IsNullOrWhiteSpace(query.State))
                {
                    if (Enum.TryParse<State>(query.State, true, out var state))
                    {
                        result = result.Where(x => x.State == state);
                    }
                }

                if (!(query.CreatedAt is null))
                {
                    result = result.Where(x => x.CreatedAt.Date == query.CreatedAt.Value.Date);
                }
            } else
            {
                query = new SearchTodo { Page = 1, PageSize = 10 };
            }
            var output = (await result.PaginateAsync(query.Page, query.PageSize)).Map(d=>d.AsDto());


            return output;
        }
    }
}
