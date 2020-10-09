using System;
using System.Collections.Generic;
using System.Text;

namespace Tito.Services.Todoes.Application.Commands
{
    public class DeleteTodo
    {
        public Guid TodoId { get; }

        public DeleteTodo(Guid todoId)
        {
            TodoId = todoId;
        }
    }
}
