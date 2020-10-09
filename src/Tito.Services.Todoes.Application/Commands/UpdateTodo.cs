using System;
using System.Collections.Generic;
using System.Text;

namespace Tito.Services.Todoes.Application.Commands
{
    public class UpdateTodo
    {
        public Guid Id { get; }

        public string Title { get; }

        public string Description { get; }

        public string Priority { get; }

        public string State { get; }


        public UpdateTodo(Guid id, string title, string description, string priority, string state)
        {
            Id =  id;
            Title = title;
            Description = description;
            Priority = priority;
            State = state;
        }

    }
}
