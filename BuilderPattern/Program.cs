using T31 = BuilderPattern.Task31;
using T32 = BuilderPattern.Task32;                               // ANOTHER TASKS 
using T33 = BuilderPattern.Task33;

var oldInvoice = new T31.Invoice(
    "INV-1", "Amr", "amr@mail.com", "0100000000",
    "1 Nile St", "Cairo", "Cairo", "11511", "Egypt",
    "9 Pyramids Rd", "Giza", "Giza", "12611", "Egypt",
    DateTime.Today, "Card", "EGP",
    1000m, 100m, 126m, 1026m);

Console.WriteLine($"Task 3.1: {oldInvoice.InvoiceId} - {oldInvoice.TotalAmount} {oldInvoice.Currency}");

var invoice32 = new T32.InvoiceBuilder()
    .InvoiceId("INV-2")
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

Console.WriteLine($"Task 3.2: {invoice32.InvoiceId} - {invoice32.TotalAmount} {invoice32.Currency}");

try
{
    new T32.InvoiceBuilder().InvoiceId("INV-X").Build();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Task 3.2 error: {ex.Message}");
}

var billing = new T33.AddressBuilder()
    .Street("1 Nile St")
    .City("Cairo")
    .Country("Egypt")
    .Build();

var shipping = new T33.AddressBuilder()
    .Street("9 Pyramids Rd")
    .City("Giza")
    .ZipCode("12611")
    .Country("Egypt")
    .Build();

var order = new T33.OrderBuilder()
    .OrderDate(DateTime.Today)
    .Currency("EGP")
    .PaymentMethod("Card")
    .SubTotal(1000m)
    .DiscountAmount(100m)
    .TaxAmount(126m)
    .Build();

var invoice33 = new T33.InvoiceBuilder()
    .InvoiceId("INV-3")
    .CustomerName("Amr")
    .CustomerEmail("amr@mail.com")
    .BillingAddress(billing)
    .ShippingAddress(shipping)
    .Order(order)
    .Build();

Console.WriteLine($"Task 3.3: {invoice33.InvoiceId} - {invoice33.Order.TotalAmount} {invoice33.Order.Currency} -> {invoice33.ShippingAddress.City}");
