namespace EntityFrameworkCore.Extensions
{
    public interface ISortable
    {
        string SortBy { get; set; }
        bool IsSortAscending { get; set; }
    }
}