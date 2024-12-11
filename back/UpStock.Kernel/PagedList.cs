namespace UpStock.Kernel;

public class PagedList<T>
{
    public MetaData MetaData { get; set; }
    public List<T> Items { get; set; }

    protected PagedList()
    {
        MetaData = new MetaData();
        Items = [];
    }

    public PagedList(IEnumerable<T> items, long count, int pageNumber, int pageSize)
    {
        MetaData = new MetaData
        {
            TotalCount = count,
            PageSize = pageSize,
            CurrentPage = pageNumber,
            TotalPages = (int)Math.Ceiling(count / (double)pageSize)
        };
        Items = [.. items];
    }

    public static PagedList<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = source.Count();

        var items = source
          .Skip((pageNumber - 1) * pageSize)
          .Take(pageSize).ToList();

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}

public sealed class MetaData
{
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; } = 0;
    public int PageSize { get; set; } = 0;
    public long TotalCount { get; set; } = 0;
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;
}

public class PaginationParameter
{
    private const int DefaultMaxElementByPage = 15;
    public int PageNumber { get; set; } = 1;
    private int _pageSize = DefaultMaxElementByPage;

    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = value > DefaultMaxElementByPage ? DefaultMaxElementByPage : value;
        }
    }
}
