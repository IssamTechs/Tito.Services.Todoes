using System; 
using Tito.Services.Todoes.Core.ValueObjects;

namespace Tito.Services.Todoes.Core.Entities
{
    // immutable
    public class Todo
    {
        public Guid Id { get; }

        public Title Title { get; }

        public Description Description { get; }

        public DateTimeOffset CreatedAt { get;}

        public Priority Priority { get; }

        public State State { get; }

        private Todo()
        {

        }
        public Todo(Guid id, Title title, Description description,
            Priority priority, State state)
        {
            Id = id;
            Title = title;
            Description = description;
            State = state;
            Priority = priority; 
        }
        public Todo(Guid id, Title title, Description description,
            Priority priority, State state, DateTimeOffset createdAt)
        {
            Id = id;
            Title = title;
            Description = description;
            State = state;
            Priority = priority;
            CreatedAt = createdAt;
        }
    }
}
