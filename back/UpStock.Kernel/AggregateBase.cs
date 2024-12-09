using System.Text.Json.Serialization;

namespace UpStock.Kernel;

/// <summary>
/// </summary>
/// <remarks>Inspired from https://martendb.io/scenarios/aggregates-events-repositories.html</remarks>
public abstract class AggregateBase
{
    // For indexing our event streams
    [JsonInclude]
    public string Id { get; protected set; } = null!;

    // For protecting the state, i.e. conflict prevention
    // The setter is only public for setting up test conditions
    public long Version { get; set; } = 0;

    // JsonIgnore - for making sure that it won't be stored in inline projection
    [JsonIgnore]
    private readonly List<object> _uncommittedEvents = [];

    // Get the deltas, i.e. events that make up the state, not yet persisted
    public IEnumerable<object> GetUncommittedEvents()
    {
        return _uncommittedEvents;
    }

    // Mark the deltas as persisted.
    public void ClearUncommittedEvents()
    {
        _uncommittedEvents.Clear();
    }

    protected void AddUncommittedEvent(object @event)
    {
        // add the event to the uncommitted list
        _uncommittedEvents.Add(@event);
    }
}
