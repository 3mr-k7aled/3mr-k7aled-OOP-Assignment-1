public class Guest
{
    private readonly List<Reservation> _reservations = new();

    public int GuestId { get; }
    public string FullName { get; }
    public string PhoneNumber { get; }

    public IReadOnlyList<Reservation> Reservations => _reservations;

    public Guest(int guestId, string fullName, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name is required.");
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number is required.");

        GuestId = guestId;
        FullName = fullName;
        PhoneNumber = phoneNumber;
    }

    public Reservation MakeReservation(int reservationId, DateTime checkIn, DateTime checkOut, Room room)
    {
        var reservation = new Reservation(reservationId, checkIn, checkOut, room);
        _reservations.Add(reservation);
        room.AddReservation(reservation);
        return reservation;
    }
}
