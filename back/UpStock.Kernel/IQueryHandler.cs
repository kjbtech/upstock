namespace UpStock.Kernel;

public interface IQuery<TResult>
{
}

public interface IQueryHandler<IQuery, TResult>
    where IQuery : IQuery<TResult>
{
    Task<TResult> HandleAsync(IQuery query);
}
