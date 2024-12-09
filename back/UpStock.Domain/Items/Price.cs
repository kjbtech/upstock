namespace UpStock.Domain.Items;

public sealed record Price
{
    public decimal Amount { get; private set; } = 0;
    public string Currency { get; private set; } = "€";

    public Price(decimal amount, string currency = "€")
    {
        Amount = amount;
        Currency = currency;
    }

    public Price(double amount, string currency = "€")
    {
        Amount = Convert.ToDecimal(amount);
        Currency = currency;
    }

    public Price ChangeAmount(decimal newAmount)
    {
        return new Price(newAmount, Currency);
    }
    public Price Add(Price price)
    {
        return new Price(price.Amount + Amount, Currency);
    }
    public Price Subtract(Price price)
    {
        return new Price(Amount - price.Amount, Currency);
    }

    public Price MultiplyAmount(decimal coef)
    {
        return new Price(coef * Amount, Currency);
    }

    public bool IsZero() => Amount == 0;

    public override string ToString()
    {
        return $"{Amount:0.00} {Currency}";
    }
}
