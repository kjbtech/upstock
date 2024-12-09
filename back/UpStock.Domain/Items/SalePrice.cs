namespace UpStock.Domain.Items;

public record SalePrice
{
    public Price PriceWithoutVat { get; private set; }
    public VatRate VatRate { get; private set; }
    public Price VatPart { get; }
    public Price PriceWithVat { get; }

    public SalePrice(Price priceWithoutVat, VatRate vatRate)
    {
        PriceWithoutVat = priceWithoutVat;
        VatRate = vatRate;
        VatPart = new Price(VatRate.CalculateVAT(PriceWithoutVat.Amount));
        PriceWithVat = PriceWithoutVat.Add(VatPart);
    }

    public override string ToString()
    {
        return $"{PriceWithoutVat} HT (TVA {VatRate})";
    }
}
