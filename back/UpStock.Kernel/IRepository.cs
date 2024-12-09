namespace UpStock.Kernel;

public interface IRepository
{
    Task StoreAsync(AggregateBase aggregate, CancellationToken cancellationToken = default);

    Task StoreAsync<TAggregate, TEvent>(string aggregateId, TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : Event
        where TAggregate : AggregateBase;

    Task<T?> LoadAsync<T>(string id, int? version = null, CancellationToken cancellationToken = default)
        where T : AggregateBase;

    Task<T?> GetAsync<T>(string id, CancellationToken cancellationToken = default)
        where T : class, new();
}
