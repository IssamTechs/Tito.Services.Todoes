using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tito.Services.Todoes.Application.Paginations
{
    public class PagedResult<T> : PagedResultsPage
    {
        public IEnumerable<T> Items { get; }

        protected PagedResult()
        {
            Items = Enumerable.Empty<T>();
        }

        protected PagedResult(IEnumerable<T> items, int currentPage, int pageSize, int totalPages, long totalResults): base 
            (currentPage, pageSize, totalPages, totalResults)
        {
            Items = items;
        }

        public static PagedResult<T> Create(IEnumerable<T> items, int currentPage, int pageSize, int totalPages, long totalResults)
            => new PagedResult<T>(items, currentPage, pageSize, totalPages, totalResults);

        public static PagedResult<T> Empty = new PagedResult<T>();

        public static PagedResult<T> From(PagedResultsPage result, IEnumerable<T> items)
            => new PagedResult<T>(items, result.CurrentPage, result.PageSize, result.TotalPages, result.TotalResults);
        public PagedResult<U> Map<U>(Func<T, U> map)
            => PagedResult<U>.From(this, Items.Select(map));
    }
}