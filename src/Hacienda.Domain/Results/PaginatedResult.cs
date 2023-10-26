namespace Hacienda.Domain.Results;

public class PaginatedResult<T>
{
    public IList<T> Items { get; }
    public int CurrentPage { get; }
    public int PageSize { get; }
    public int TotalItems { get; }

    public PaginatedResult(IList<T> items, int currentPage, int pageSize, int totalItems)
    {
        Items = items;
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalItems = totalItems;
    }
}