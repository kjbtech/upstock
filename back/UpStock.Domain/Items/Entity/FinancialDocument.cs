namespace UpStock.Domain.Items.Entity;

public record FinancialDocument
{
    public string VendorName { get; set; }
    public string Number { get; set; }
    public DateOnly? Date { get; set; }

    public FinancialDocument(string vendorName, string number, string date)
    {
        VendorName = vendorName;
        Number = number;
        Date = string.IsNullOrWhiteSpace(date) ? null : DateOnly.Parse(date);
    }
}
