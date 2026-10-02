namespace BuilderPattern.Task33;

public class InvoiceBuilder
{
    private string _invoiceId, _customerName, _customerEmail, _customerPhone;
    private Address _billingAddress, _shippingAddress;
    private OrderDetails _order;

    public InvoiceBuilder InvoiceId(string v) { _invoiceId = v; return this; }
    public InvoiceBuilder CustomerName(string v) { _customerName = v; return this; }
    public InvoiceBuilder CustomerEmail(string v) { _customerEmail = v; return this; }
    public InvoiceBuilder CustomerPhone(string v) { _customerPhone = v; return this; }
    public InvoiceBuilder BillingAddress(Address v) { _billingAddress = v; return this; }
    public InvoiceBuilder ShippingAddress(Address v) { _shippingAddress = v; return this; }
    public InvoiceBuilder Order(OrderDetails v) { _order = v; return this; }

    public Invoice Build()
    {
        if (string.IsNullOrWhiteSpace(_invoiceId)) throw new InvalidOperationException("InvoiceId is required.");
        if (string.IsNullOrWhiteSpace(_customerName)) throw new InvalidOperationException("CustomerName is required.");
        if (string.IsNullOrWhiteSpace(_customerEmail)) throw new InvalidOperationException("CustomerEmail is required.");
        if (_billingAddress is null) throw new InvalidOperationException("BillingAddress is required.");
        if (_order is null) throw new InvalidOperationException("Order is required.");

        return new Invoice
        {
            InvoiceId = _invoiceId,
            CustomerName = _customerName,
            CustomerEmail = _customerEmail,
            CustomerPhone = _customerPhone,
            BillingAddress = _billingAddress,
            ShippingAddress = _shippingAddress ?? _billingAddress,
            Order = _order
        };
    }
}
