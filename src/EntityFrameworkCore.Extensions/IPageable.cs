namespace EntityFrameworkCore.Extensions
{
    public interface IPageable
    {
        int Page { get; set; }
        int PageSize { get; set; }
    }
}