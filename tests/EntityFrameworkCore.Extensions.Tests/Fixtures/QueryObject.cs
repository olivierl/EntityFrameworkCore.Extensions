namespace EntityFrameworkCore.Extensions.Tests.Fixtures
{
    public class QueryObject : ISortable, IPageable
    {
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}