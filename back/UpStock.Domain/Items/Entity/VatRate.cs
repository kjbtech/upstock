namespace UpStock.Domain.Items.Entity;

public record VatRate
{
    public decimal Rate { get; private set; }

    public VatRate(decimal rate)
    {
        if (rate < 0) rate = 0;
        if (rate > 100) rate = 100;
        Rate = rate;
    }

    public decimal CalculateVAT(decimal amount)
    {
        return amount * Rate / 100;
    }

    public override string ToString()
    {
        return $"{Rate / 100:P2}"; // E.g., "20.00 %"
    }
}
