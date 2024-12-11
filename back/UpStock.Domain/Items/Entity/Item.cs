using UpStock.Kernel;

namespace UpStock.Domain.Items.Entity;

public class Item : AggregateBase
{
    public FinancialDocument From { get; private set; }
    public string? Description { get; private set; }

    public string? ProductCode { get; private set; }

    public double? Quantity { get; private set; }

    public SalePrice? SalePrice { get; private set; }

    protected Item()
    { }

    protected void Apply(ExtractedFromFile extractedFromFile)
    {
        Id = extractedFromFile.ItemId;
        From = extractedFromFile.From;
        Description = extractedFromFile.Description;
        ProductCode = extractedFromFile.ProductCode;
        Quantity = extractedFromFile.Quantity;
        SalePrice = extractedFromFile.SalePrice;
    }
}

public record ExtractedFromFile(
    string ItemId,
    FinancialDocument From,
    string? Description,
    string? ProductCode,
    double? Quantity,
    SalePrice? SalePrice) : Event;
