namespace BuilderPattern.Task33;

public class Invoice
{
    public string InvoiceId { get; init; }
    public string CustomerName { get; init; }
    public string CustomerEmail { get; init; }
    public string CustomerPhone { get; init; }
    public Address BillingAddress { get; init; }
    public Address ShippingAddress { get; init; }
    public OrderDetails Order { get; init; }
}
