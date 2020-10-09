using System;
using System.Collections.Generic;
using System.Text;

namespace Tito.Services.Todoes.Application.Commands
{
    public class AddTodo
    {
        public Guid Id { get; }

        public string Title { get; }

        public string Description { get; }

        public string Priority { get; }

        public string State { get; }

        public DateTimeOffset CreatedAt { get; }

        public AddTodo(Guid id, string title, string description, string priority)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            Title = title;
            Description = description;
            Priority = priority;
            CreatedAt = DateTimeOffset.UtcNow;
        }

    }
}
