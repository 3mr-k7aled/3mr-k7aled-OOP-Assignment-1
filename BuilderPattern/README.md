# 03 - Why Are 20-Parameter Constructors a Problem? (Builder Pattern)

Language: C# (.NET)

---

## Task 3.1 - The Class

```csharp
public class Invoice
{
    public string InvoiceId { get; }
    public string CustomerName { get; }
    public string CustomerEmail { get; }
    public string CustomerPhone { get; }

    public string BillingStreet { get; }
    public string BillingCity { get; }
    public string BillingState { get; }
    public string BillingZipCode { get; }
    public string BillingCountry { get; }

    public string ShippingStreet { get; }
    public string ShippingCity { get; }
    public string ShippingState { get; }
    public string ShippingZipCode { get; }
    public string ShippingCountry { get; }

    public DateTime OrderDate { get; }
    public string PaymentMethod { get; }
    public string Currency { get; }
    public decimal SubTotal { get; }
    public decimal DiscountAmount { get; }
    public decimal TaxAmount { get; }
    public decimal TotalAmount { get; }

    public Invoice(
        string invoiceId, string customerName, string customerEmail, string customerPhone,
        string billingStreet, string billingCity, string billingState, string billingZipCode, string billingCountry,
        string shippingStreet, string shippingCity, string shippingState, string shippingZipCode, string shippingCountry,
        DateTime orderDate, string paymentMethod, string currency,
        decimal subTotal, decimal discountAmount, decimal taxAmount, decimal totalAmount)
    {
        InvoiceId = invoiceId;
        CustomerName = customerName;
        CustomerEmail = customerEmail;
        CustomerPhone = customerPhone;
        BillingStreet = billingStreet;
        BillingCity = billingCity;
        BillingState = billingState;
        BillingZipCode = billingZipCode;
        BillingCountry = billingCountry;
        ShippingStreet = shippingStreet;
        ShippingCity = shippingCity;
        ShippingState = shippingState;
        ShippingZipCode = shippingZipCode;
        ShippingCountry = shippingCountry;
        OrderDate = orderDate;
        PaymentMethod = paymentMethod;
        Currency = currency;
        SubTotal = subTotal;
        DiscountAmount = discountAmount;
        TaxAmount = taxAmount;
        TotalAmount = totalAmount;
    }
}
```

The call site:

```csharp
var invoice = new Invoice(
    "INV-1", "Amr", "amr@mail.com", "0100000000",
    "1 Nile St", "Cairo", "Cairo", "11511", "Egypt",
    "9 Pyramids Rd", "Giza", "Giza", "12611", "Egypt",
    DateTime.Today, "Card", "EGP",
    1000m, 100m, 126m, 1026m);
```

### Questions

**1. Why is a single 20-parameter constructor a problem?**

- **Readability:** the call site is a wall of values with no names. You cannot tell what `"Cairo", "Cairo", "11511"` means without opening the constructor.
- **Wrong-order bugs:** many parameters share a type. Swapping `billingCity` with `shippingCity`, or `discountAmount` with `taxAmount`, compiles fine and silently produces bad data.
- **Adding a property:** every call site in the codebase must change, even for an optional field, and every optional value still has to be passed as `null` or a default.

**2. Is it purely a "constructor is too long" problem?**

No. The long constructor is a symptom of a deeper design issue: the class does too many jobs. It holds customer data, two addresses, and order/payment data. This violates the Single Responsibility Principle. The billing and shipping fields are the same concept (an address) copy-pasted twice. The fix is to extract an `Address` and an `OrderDetails` class, and the Builder pattern then makes construction readable and safe.

---

## Task 3.2 - Solve With a Builder

**Mandatory:** InvoiceId, CustomerName, CustomerEmail, billing street / city / country, OrderDate, Currency, SubTotal.

**Optional:** CustomerPhone, state, zip codes, shipping address (defaults to billing), PaymentMethod, DiscountAmount, TaxAmount. TotalAmount is calculated.

```csharp
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
```

The call site:

```csharp
var invoice = new InvoiceBuilder()
    .InvoiceId("INV-1")
    .CustomerName("Amr")
    .CustomerEmail("amr@mail.com")
    .BillingStreet("1 Nile St")
    .BillingCity("Cairo")
    .BillingCountry("Egypt")
    .OrderDate(DateTime.Today)
    .Currency("EGP")
    .SubTotal(1000m)
    .DiscountAmount(100m)
    .TaxAmount(126m)
    .Build();
```

Forgetting a mandatory property fails immediately:

```csharp
new InvoiceBuilder().InvoiceId("INV-1").Build();
```

```
InvalidOperationException: CustomerName is required.
```

---

## Task 3.3 - Refactor Into Smaller, Composed Builders

```csharp
public class Address
{
    public string Street { get; init; }
    public string City { get; init; }
    public string State { get; init; }
    public string ZipCode { get; init; }
    public string Country { get; init; }
}

public class AddressBuilder
{
    private string _street, _city, _state, _zipCode, _country;

    public AddressBuilder Street(string v) { _street = v; return this; }
    public AddressBuilder City(string v) { _city = v; return this; }
    public AddressBuilder State(string v) { _state = v; return this; }
    public AddressBuilder ZipCode(string v) { _zipCode = v; return this; }
    public AddressBuilder Country(string v) { _country = v; return this; }

    public Address Build()
    {
        if (string.IsNullOrWhiteSpace(_street)) throw new InvalidOperationException("Street is required.");
        if (string.IsNullOrWhiteSpace(_city)) throw new InvalidOperationException("City is required.");
        if (string.IsNullOrWhiteSpace(_country)) throw new InvalidOperationException("Country is required.");

        return new Address
        {
            Street = _street,
            City = _city,
            State = _state,
            ZipCode = _zipCode,
            Country = _country
        };
    }
}

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
```

The call site:

```csharp
var billing = new AddressBuilder()
    .Street("1 Nile St")
    .City("Cairo")
    .Country("Egypt")
    .Build();

var shipping = new AddressBuilder()
    .Street("9 Pyramids Rd")
    .City("Giza")
    .ZipCode("12611")
    .Country("Egypt")
    .Build();

var order = new OrderBuilder()
    .OrderDate(DateTime.Today)
    .Currency("EGP")
    .PaymentMethod("Card")
    .SubTotal(1000m)
    .DiscountAmount(100m)
    .TaxAmount(126m)
    .Build();

var invoice = new InvoiceBuilder()
    .InvoiceId("INV-1")
    .CustomerName("Amr")
    .CustomerEmail("amr@mail.com")
    .BillingAddress(billing)
    .ShippingAddress(shipping)
    .Order(order)
    .Build();
```

### Why is the composed version better than the single big builder?

- **Single responsibility:** `AddressBuilder` owns only address data and its rules. `OrderBuilder` owns only order/payment data and the total calculation. `InvoiceBuilder` only ties customer info, addresses and the order together.
- **Independent validation:** `AddressBuilder.Build()` guarantees a complete address (street, city, country) on its own. `Invoice` knows nothing about address rules, so those rules live in exactly one place.
- **Reuse:** the same `AddressBuilder` creates both billing and shipping addresses. In Task 3.2 the five address methods and their validation had to be written twice, once with the `Billing` prefix and once with `Shipping`. A new address field is now added in one place instead of two.
- **Call-site readability:** the single builder is one flat chain of about 20 calls that mixes address, customer and order fields. The composed version reads as three small named steps (billing/shipping addresses, order, invoice), and each step is easy to test and understand on its own.
