using System;
using System.Collections.Generic;
using System.Text;

namespace Tito.Services.Todoes.Application.DTO
{
    public class TodoDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string priority { get; set; }

        public string State { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
