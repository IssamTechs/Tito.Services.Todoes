using System;
using System.Collections.Generic;
using System.Text;

namespace Tito.Services.Todoes.Application.Exceptions
{
    public class InvalidTodoPriorityException : AppException
    {
        public override string Code => "invalid_todo_priority";
        public string Priority { get; }

        public InvalidTodoPriorityException(string priority): base($"Invalid todo priority: {priority}")
        {
            Priority = priority;
        }
    }
}
