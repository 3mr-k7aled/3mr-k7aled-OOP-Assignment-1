namespace BuilderPattern.Task33;

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
