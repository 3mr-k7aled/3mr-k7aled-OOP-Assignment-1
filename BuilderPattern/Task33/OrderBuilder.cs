namespace BuilderPattern.Task33;

public class OrderBuilder
{
    private DateTime? _orderDate;
    private string _paymentMethod, _currency;
    private decimal? _subTotal;
    private decimal _discountAmount, _taxAmount;

    public OrderBuilder OrderDate(DateTime v) { _orderDate = v; return this; }
    public OrderBuilder PaymentMethod(string v) { _paymentMethod = v; return this; }
    public OrderBuilder Currency(string v) { _currency = v; return this; }
    public OrderBuilder SubTotal(decimal v) { _subTotal = v; return this; }
    public OrderBuilder DiscountAmount(decimal v) { _discountAmount = v; return this; }
    public OrderBuilder TaxAmount(decimal v) { _taxAmount = v; return this; }

    public OrderDetails Build()
    {
        if (_orderDate is null) throw new InvalidOperationException("OrderDate is required.");
        if (_subTotal is null) throw new InvalidOperationException("SubTotal is required.");
        if (string.IsNullOrWhiteSpace(_currency)) throw new InvalidOperationException("Currency is required.");

        return new OrderDetails
        {
            OrderDate = _orderDate.Value,
            PaymentMethod = _paymentMethod,
            Currency = _currency,
            SubTotal = _subTotal.Value,
            DiscountAmount = _discountAmount,
            TaxAmount = _taxAmount,
            TotalAmount = _subTotal.Value - _discountAmount + _taxAmount
        };
    }
}
