namespace BuilderPattern.Task32;

public class Invoice
{
    public string InvoiceId { get; init; }
    public string CustomerName { get; init; }
    public string CustomerEmail { get; init; }
    public string CustomerPhone { get; init; }

    public string BillingStreet { get; init; }
    public string BillingCity { get; init; }
    public string BillingState { get; init; }
    public string BillingZipCode { get; init; }
    public string BillingCountry { get; init; }

    public string ShippingStreet { get; init; }
    public string ShippingCity { get; init; }
    public string ShippingState { get; init; }
    public string ShippingZipCode { get; init; }
    public string ShippingCountry { get; init; }

    public DateTime OrderDate { get; init; }
    public string PaymentMethod { get; init; }
    public string Currency { get; init; }
    public decimal SubTotal { get; init; }
    public decimal DiscountAmount { get; init; }
    public decimal TaxAmount { get; init; }
    public decimal TotalAmount { get; init; }
}
