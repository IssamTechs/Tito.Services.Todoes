namespace Tito.Services.Todoes.Application.Paginations
{
    public class PagedQueryBase
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public string SortOrder { get; set; }
    }
}