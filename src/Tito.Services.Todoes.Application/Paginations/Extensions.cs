using System;
using System.Linq;
using System.Threading.Tasks;
using Tito.Services.Todoes.Application.Paginations;

namespace Tito.Services.Todoes.Application.Queries
{
    public static class Extensions
    {
        public static async Task<PagedResult<T>> PaginateAsync<T>(this IQueryable<T> collection, PagedQueryBase query)
            => await collection.PaginateAsync(query.Page, query.PageSize);

        public static async Task<PagedResult<T>> PaginateAsync<T>(this IQueryable<T> collection, int page = 1, int pageSize = 10)
        {
            if (page <= 0)
                page = 1;
            
            if (pageSize <= 0)
                pageSize = 10;
            
            var isEmpty = collection.Any() == false;

            if (isEmpty)
                return PagedResult<T>.Empty;

            var totalResults = collection.Count();

            var totalPages = (int)Math.Ceiling((decimal)totalResults / pageSize);

            var data = collection.Limit(page, pageSize).ToList();

            return PagedResult<T>.Create(data, page, pageSize, totalPages, totalResults);
        } 

        public static IQueryable<T> Limit<T> (this IQueryable<T> collection, int page = 1, int pageSize = 10)
        {
            if (page <= 0)
                page = 1;
            if (pageSize <= 0)
                pageSize = 10;

            var skip = (page - 1) * pageSize;
            var data = collection.Skip(skip).Take(pageSize);

            return data;

        }

        public static IQueryable<T> Limit<T>(this IQueryable<T> collection, PagedQueryBase query)
            => collection.Limit(query.Page, query.PageSize);

    }
}
