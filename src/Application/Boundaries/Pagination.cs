using Optional;

namespace Application.Boundaries;

public class PaginationInput
{
    public int PageNumber { get; set; } = 1;
    public int ItemsPerPage { get; set; } = 15;

    public int PageSkip() => PageNumber.SomeWhen(x => x >= 0).Match((x) => x, () => 1) - 1;
}

public class PaginationInputObject<T> : PaginationInput
{
    public T Input { get; set; }

    public PaginationInputObject(T input)
    {
        Input = input;
    }
}

public class PaginationOutput<T> : PaginationInput
{
    public List<T> Items { get; set; } = new List<T>();
    public long TotalItems { get; set; }

    public PaginationOutput(List<T> items, long totalItems, int pageNumber, int itemsPerPage)
    {
        Items = items;
        TotalItems = totalItems;
        ItemsPerPage = itemsPerPage;
        pageNumber = PageNumber;
    }
}