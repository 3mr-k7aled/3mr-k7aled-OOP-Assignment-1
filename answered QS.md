Questions

1. Why is a single 20-parameter constructor a problem?

- Readability: the call site is a wall of values with no names. You cannot tell what Cairo, Cairo, 11511 means without opening the constructor.
- Wrong order bugs: many parameters share a type Swapping billingCity with shippingCity, or discountAmount with taxAmount, compiles fine and silently produces bad data.
- Adding a property: every call site in the codebase must change, even for an optional field, and every optional value still has to be passed as null or a default.

2. Is it purely a constructor is too long problem?

No. The long constructor is a symptom of a deeper design issue: the class does too many jobs. It holds customer data, two addresses, and order , payment data. This violates the Single Responsibility Principle. The billing and shipping fields are the same concept (an address) copy-pasted twice. The fix is to extract an Address and an OrderDetails class, and the Builder pattern then makes construction readable and safe.

---

Task 3.2

Why is the composed version better than the single big builder?

- Single responsibility: AddressBuilder owns only address data and its rules. OrderBuilder owns only order/payment data and the total calculation. InvoiceBuilder only ties customer info, addresses and the order together.
- Independent validation: AddressBuilder.Build() guarantees a complete address (street, city, country) on its own. Invoice knows nothing about address rules, so those rules live in exactly one place.
- Reuse: the same AddressBuilder creates both billing and shipping addresses. In Task 3.2 the five address methods and their validation had to be written twice, once with the Billing prefix and once with Shipping. A new address field is now added in one place instead of two.
- Call-site readability: the single builder is one flat chain of about 20 calls that mixes address, customer and order fields. The composed version reads as three small named steps (billing/shipping addresses, order, invoice), and each step is easy to test and understand on its own.
