namespace Hacienda.Domain.Results;

/// <summary>
/// PAra devolver una lista de elementos paginados.
/// </summary>
/// <typeparam name="T">Tipo de elemento a paginar</typeparam>
public class ResultPaginated<T>
{
    public IList<T> Items { get; }
    public int CurrentPage { get; }
    public int PageSize { get; }
    public int TotalItems { get; }

    public ResultPaginated(IList<T> items, int currentPage, int pageSize, int totalItems)
    {
        Items = items;
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalItems = totalItems;
    }
}