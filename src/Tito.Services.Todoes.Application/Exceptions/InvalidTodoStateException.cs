using System;
using System.Collections.Generic;
using System.Text;

namespace Tito.Services.Todoes.Application.Exceptions
{
    public class InvalidTodoStateException : AppException
    {
        public override string Code => "invalid_todo_state";
        public string State { get; }

        public InvalidTodoStateException(string state) : base($"Invalid todo state: {state}")
        {
            State = state;
        }
    }
}
