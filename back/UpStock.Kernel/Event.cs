namespace UpStock.Kernel;

public abstract record Event
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTimeOffset OperationDate { get; init; } = DateTimeOffset.Now;
}
