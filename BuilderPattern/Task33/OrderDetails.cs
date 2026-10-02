namespace BuilderPattern.Task33;

public class OrderDetails
{
    public DateTime OrderDate { get; init; }
    public string PaymentMethod { get; init; }
    public string Currency { get; init; }
    public decimal SubTotal { get; init; }
    public decimal DiscountAmount { get; init; }
    public decimal TaxAmount { get; init; }
    public decimal TotalAmount { get; init; }
}
