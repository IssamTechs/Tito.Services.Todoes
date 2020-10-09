using System;
using System.Collections.Generic;
using System.Text;

namespace Tito.Services.Todoes.Application.Paginations
{
    public class PagedResultsPage
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public long TotalResults { get; }
        protected PagedResultsPage() { }
        protected PagedResultsPage(int currentPage, int pageSize, int totalpages, long totalResults)
        {
            CurrentPage = currentPage > totalpages ? totalpages : currentPage;
            PageSize = pageSize;
            TotalPages = totalpages;
            TotalResults = totalResults;
        }

    }
}
