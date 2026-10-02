namespace BuilderPattern.Task32;

public class InvoiceBuilder
{
    private string _invoiceId, _customerName, _customerEmail, _customerPhone;
    private string _billingStreet, _billingCity, _billingState, _billingZipCode, _billingCountry;
    private string _shippingStreet, _shippingCity, _shippingState, _shippingZipCode, _shippingCountry;
    private DateTime? _orderDate;
    private string _paymentMethod, _currency;
    private decimal? _subTotal;
    private decimal _discountAmount, _taxAmount;

    public InvoiceBuilder InvoiceId(string v) { _invoiceId = v; return this; }
    public InvoiceBuilder CustomerName(string v) { _customerName = v; return this; }
    public InvoiceBuilder CustomerEmail(string v) { _customerEmail = v; return this; }
    public InvoiceBuilder CustomerPhone(string v) { _customerPhone = v; return this; }

    public InvoiceBuilder BillingStreet(string v) { _billingStreet = v; return this; }
    public InvoiceBuilder BillingCity(string v) { _billingCity = v; return this; }
    public InvoiceBuilder BillingState(string v) { _billingState = v; return this; }
    public InvoiceBuilder BillingZipCode(string v) { _billingZipCode = v; return this; }
    public InvoiceBuilder BillingCountry(string v) { _billingCountry = v; return this; }

    public InvoiceBuilder ShippingStreet(string v) { _shippingStreet = v; return this; }
    public InvoiceBuilder ShippingCity(string v) { _shippingCity = v; return this; }
    public InvoiceBuilder ShippingState(string v) { _shippingState = v; return this; }
    public InvoiceBuilder ShippingZipCode(string v) { _shippingZipCode = v; return this; }
    public InvoiceBuilder ShippingCountry(string v) { _shippingCountry = v; return this; }

    public InvoiceBuilder OrderDate(DateTime v) { _orderDate = v; return this; }
    public InvoiceBuilder PaymentMethod(string v) { _paymentMethod = v; return this; }
    public InvoiceBuilder Currency(string v) { _currency = v; return this; }
    public InvoiceBuilder SubTotal(decimal v) { _subTotal = v; return this; }
    public InvoiceBuilder DiscountAmount(decimal v) { _discountAmount = v; return this; }
    public InvoiceBuilder TaxAmount(decimal v) { _taxAmount = v; return this; }

    public Invoice Build()
    {
        Require(_invoiceId, "InvoiceId");
        Require(_customerName, "CustomerName");
        Require(_customerEmail, "CustomerEmail");
        Require(_billingStreet, "BillingStreet");
        Require(_billingCity, "BillingCity");
        Require(_billingCountry, "BillingCountry");
        Require(_currency, "Currency");
        if (_orderDate is null) throw new InvalidOperationException("OrderDate is required.");
        if (_subTotal is null) throw new InvalidOperationException("SubTotal is required.");

        return new Invoice
        {
            InvoiceId = _invoiceId,
            CustomerName = _customerName,
            CustomerEmail = _customerEmail,
            CustomerPhone = _customerPhone,
            BillingStreet = _billingStreet,
            BillingCity = _billingCity,
            BillingState = _billingState,
            BillingZipCode = _billingZipCode,
            BillingCountry = _billingCountry,
            ShippingStreet = _shippingStreet ?? _billingStreet,
            ShippingCity = _shippingCity ?? _billingCity,
            ShippingState = _shippingState ?? _billingState,
            ShippingZipCode = _shippingZipCode ?? _billingZipCode,
            ShippingCountry = _shippingCountry ?? _billingCountry,
            OrderDate = _orderDate.Value,
            PaymentMethod = _paymentMethod,
            Currency = _currency,
            SubTotal = _subTotal.Value,
            DiscountAmount = _discountAmount,
            TaxAmount = _taxAmount,
            TotalAmount = _subTotal.Value - _discountAmount + _taxAmount
        };
    }

    private static void Require(string value, string name)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException($"{name} is required.");
    }
}
