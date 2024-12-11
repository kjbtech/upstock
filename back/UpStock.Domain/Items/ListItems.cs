using UpStock.Domain.Items.Entity;
using UpStock.Kernel;

namespace UpStock.Domain.Items;

public sealed record ListItems(PaginationParameter PaginationParameter)
    : IQuery<ItemsList>
{
}

public sealed class ItemSummary
{
    public string Id { get; set; } = null!;
    public DateOnly AddedDate { get; set; }
    public string Description { get; set; } = null!;
    public double? Quantity { get; set; }

    protected void Apply(ExtractedFromFile extractedFromFile)
    {
        Id = extractedFromFile.ItemId;
        AddedDate = DateOnly.FromDateTime(extractedFromFile.OperationDate.Date);
        Description = extractedFromFile.Description ?? "";
        Quantity = extractedFromFile.Quantity;
    }
}

public sealed class ItemsList : PagedList<ItemSummary>
{
    public ItemsList() : base()
    {
    }

    public ItemsList(IEnumerable<ItemSummary> items, long count, int pageNumber, int pageSize)
        : base(items, count, pageNumber, pageSize)
    {
    }
}
