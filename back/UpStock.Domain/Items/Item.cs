using UpStock.Kernel;

namespace UpStock.Domain.Items;

public class Item : AggregateBase
{
    public string? Description { get; private set; }

    public string? ProductCode { get; private set; }

    public double? Quantity { get; private set; }

    public SalePrice? SalePrice { get; private set; }

    protected Item()
    { }

    protected void Apply(ExtractedFromFile extractedFromFile)
    {
        Id = extractedFromFile.ItemId;
        Description = extractedFromFile.Description;
        ProductCode = extractedFromFile.ProductCode;
        Quantity = extractedFromFile.Quantity;
        SalePrice = extractedFromFile.SalePrice;
    }
}

public record ExtractedFromFile(
    string ItemId,
    string? Description,
    string? ProductCode,
    double? Quantity,
    SalePrice? SalePrice) : Event;
