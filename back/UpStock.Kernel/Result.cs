namespace UpStock.Kernel;

/// <summary>
/// Hold no object but
/// identify if success or failure.
/// </summary>
public class Result
{
    /// <summary>
    /// If the result is successful, will be set to true.
    /// Else, false.
    /// </summary>
    public bool IsSuccess { get; private set; }

    public bool HasFailed => !IsSuccess;

    /// <summary>
    /// Not null if success is false.
    /// </summary>
    public Errors? Errors { get; private set; }

    private Result()
    {
    }

    /// <summary>
    /// Create a successful result.
    /// </summary>
    public static Result Success()
    {
        return new Result
        {
            IsSuccess = true,
        };
    }

    /// <summary>
    /// Create a failure result.
    /// </summary>
    public static Result Failure()
    {
        return new Result()
        {
            IsSuccess = false
        };
    }

    /// <summary>
    /// Create a failure result.
    /// </summary>
    public static Result Failure(Error? error)
    {
        ArgumentNullException.ThrowIfNull(error);

        return new Result()
        {
            Errors = [error],
            IsSuccess = false,
        };
    }

    /// <summary>
    /// Create a failure result from a list or errors.
    /// </summary>
    public static Result Failure(Errors? errors)
    {
        ArgumentNullException.ThrowIfNull(errors);

        return new Result()
        {
            Errors = errors,
            IsSuccess = false,
        };
    }
}

/// <summary>
/// Hold a value object or an error object,
/// depending if success or failure.
/// </summary>
/// <typeparam name="T">The value if success.</typeparam>
/// <remarks>Naive implementation of Either principle.</remarks>
public class Result<T>
    where T : class
{
    /// <summary>
    /// Not null if success is true.
    /// </summary>
    public T? Value { get; private set; }

    /// <summary>
    /// Not null if success is false.
    /// </summary>
    public Errors? Errors { get; private set; }

    /// <summary>
    /// If the result is successful, will be set to true.
    /// Else, false.
    /// </summary>
    public bool IsSuccess { get; private set; }

    public bool HasFailed => !IsSuccess;

    private Result()
    {
        Value = null;
        Errors = null;
    }

    /// <summary>
    /// Create a successful result.
    /// </summary>
    public static Result<T> Success(T? value)
    {
        ArgumentNullException.ThrowIfNull(value);

        return new Result<T>()
        {
            Value = value,
            IsSuccess = true
        };
    }

    /// <summary>
    /// Create a failure result.
    /// </summary>
    public static Result<T> Failure(Error? error)
    {
        ArgumentNullException.ThrowIfNull(error);

        return new Result<T>()
        {
            Errors = [error],
            IsSuccess = false,
        };
    }

    /// <summary>
    /// Create a failure result from a list or errors.
    /// </summary>
    public static Result<T> Failure(Errors? errors)
    {
        ArgumentNullException.ThrowIfNull(errors);

        return new Result<T>()
        {
            Errors = errors,
            IsSuccess = false,
        };
    }
}
