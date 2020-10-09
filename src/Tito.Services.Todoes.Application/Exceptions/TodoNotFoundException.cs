using System;
using System.Collections.Generic;
using System.Text;

namespace Tito.Services.Todoes.Application.Exceptions
{
    public class TodoNotFoundException : AppException
    {
        public override string Code => "todo_not_found";
        public Guid Id { get; }

        public TodoNotFoundException(Guid id) : base($"Could not find todo with id = '{id}'")
        {
            Id = id;
        }
    }
}
