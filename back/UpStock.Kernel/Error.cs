namespace UpStock.Kernel;

public class Errors : List<Error>
{
}

public record Error
{
    public string Description { get; set; }

    public Error(string description)
    {
        Description = description;
    }
}
