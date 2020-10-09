using System;
using Tito.Services.Todoes.Application.Paginations;

namespace Tito.Services.Todoes.Application.Queries
{
    public class SearchTodo: PagedQueryBase
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }

        public string Priority { get; set; }

        public string State { get; set; }
    }
}
