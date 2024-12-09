using JasperFx.Core;
using Marten;
using UpStock.Domain;
using UpStock.Kernel;

namespace UpStock.Infrastructure;

/// <summary>
/// see https://martendb.io/configuration/hostbuilder.html#working-with-multiple-marten-databases
/// </summary>
public interface IUpstockStore : IDocumentStore
{
}

/// <summary>
///
/// </summary>
/// <remarks>See https://martendb.io/scenarios/aggregates-events-repositories.html</remarks>
internal sealed class MartenRepository : IUpstockRepository
{
    private readonly IUpstockStore _store;

    public MartenRepository(IUpstockStore store)
    {
        _store = store;
    }

    public async Task StoreAsync(AggregateBase aggregate, CancellationToken cancellationToken = default)
    {
        // Take non-persisted events, push them to the event stream, indexed by the aggregate ID
        var events = aggregate.GetUncommittedEvents().ToArray();
        await using var session = _store.LightweightSession();
        session.Events.Append(aggregate.Id, aggregate.Version, events);

        await session.SaveChangesAsync(cancellationToken);

        // Once successfully persisted, clear events from list of uncommitted events
        aggregate.ClearUncommittedEvents();
    }

    public async Task StoreAsync<TAggregate, TEvent>(string aggregateId, TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : Event
        where TAggregate : AggregateBase
    {
        await using var session = _store.LightweightSession();
        if (await session.Events.AggregateStreamAsync<TAggregate>(aggregateId) == null)
        {
            session.Events.StartStream(typeof(TAggregate), aggregateId, @event);
        }
        else
        {
            session.Events.Append(aggregateId, @event);
        }

        await session.SaveChangesAsync(cancellationToken);
    }

    public async Task<T?> LoadAsync<T>(string id, int? version = null, CancellationToken cancellationToken = default)
        where T : AggregateBase
    {
        await using var session = _store.LightweightSession();
        var aggregate = await session.Events.AggregateStreamAsync<T>(id, version ?? 0, token: cancellationToken);
        return aggregate;
    }

    public async Task<T?> GetAsync<T>(string id, CancellationToken cancellationToken = default)
        where T : class, new()
    {
        await using var session = _store.LightweightSession();
        var entity = await session.LoadAsync<T>(id, token: cancellationToken);
        return entity;
    }
}
