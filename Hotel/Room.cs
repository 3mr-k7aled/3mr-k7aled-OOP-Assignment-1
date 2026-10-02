public class Room
{
    private readonly List<Reservation> _reservations = new();

    public int RoomNumber { get; }
    public string RoomType { get; }
    public decimal NightlyRate { get; private set; }
    public bool IsUnderMaintenance { get; private set; }

    public Room(int roomNumber, string roomType, decimal nightlyRate)
    {
        if (roomType != "Single" && roomType != "Double" && roomType != "Suite")
            throw new ArgumentException("Room type must be Single, Double or Suite.");
        if (nightlyRate <= 0)
            throw new ArgumentException("Nightly rate must be positive.");

        RoomNumber = roomNumber;
        RoomType = roomType;
        NightlyRate = nightlyRate;
    }

    public void ChangeNightlyRate(decimal newRate)
    {
        if (newRate <= 0)
            throw new ArgumentException("Nightly rate must be positive.");

        NightlyRate = newRate;
    }

    public void StartMaintenance()
    {
        IsUnderMaintenance = true;
    }

    public void EndMaintenance()
    {
        IsUnderMaintenance = false;
    }

    public bool IsAvailable(DateTime checkIn, DateTime checkOut)
    {
        foreach (var r in _reservations)
        {
            bool active = r.Status != "Cancelled" &&
                          r.Status != "CheckedOut";

            if (active && checkIn < r.CheckOutDate && r.CheckInDate < checkOut)
                return false;
        }
        return true;
    }

    internal void AddReservation(Reservation reservation)
    {
        _reservations.Add(reservation);
    }
}
