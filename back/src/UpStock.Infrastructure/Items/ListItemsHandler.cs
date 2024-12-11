using Marten.Events.Aggregation;
using Marten.Pagination;
using Marten;
using UpStock.Kernel;
using UpStock.Domain.Items;

namespace UpStock.Infrastructure.Items;

internal sealed class ListItemsHandler
    : IQueryHandler<ListItems, ItemsList>
{
    private readonly IUpstockStore _storage;

    public ListItemsHandler(
        IUpstockStore storage)
    {
        _storage = storage;
    }

    public async Task<ItemsList> HandleAsync(ListItems query)
    {
        await using var session = _storage.LightweightSession();
        var invoices = await session.Query<ItemSummary>()
            .ToPagedListAsync(query.PaginationParameter.PageNumber, query.PaginationParameter.PageSize);

        var totalCount = await session.Query<ItemSummary>().CountAsync();

        return new ItemsList(
            invoices,
            totalCount,
            query.PaginationParameter.PageNumber,
            query.PaginationParameter.PageSize);
    }
}

public class ItemSummaryProjection : SingleStreamProjection<ItemSummary>
{
}
