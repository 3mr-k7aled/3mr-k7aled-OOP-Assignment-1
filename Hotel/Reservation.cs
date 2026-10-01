public class Reservation
{
    public int ReservationId { get; }
    public DateTime CheckInDate { get; }
    public DateTime CheckOutDate { get; }
    public Room Room { get; }
    public string Status { get; private set; }

    public decimal TotalCost => (CheckOutDate - CheckInDate).Days * Room.NightlyRate;

    internal Reservation(int reservationId, DateTime checkIn, DateTime checkOut, Room room)
    {
        if (room == null)
            throw new ArgumentNullException(nameof(room));
        if (checkOut <= checkIn)
            throw new ArgumentException("Check-out must be after check-in.");
        if (room.IsUnderMaintenance)
            throw new InvalidOperationException("Room is under maintenance.");
        if (!room.IsAvailable(checkIn, checkOut))
            throw new InvalidOperationException("Room is already booked for these dates.");

        ReservationId = reservationId;
        CheckInDate = checkIn;
        CheckOutDate = checkOut;
        Room = room;
        Status = "Pending";
    }

    public void Confirm()
    {
        if (Status != "Pending")
            throw new InvalidOperationException("Only a pending reservation can be confirmed.");

        Status = "Confirmed";
    }

    public void CheckIn()
    {
        if (Status != "Confirmed")
            throw new InvalidOperationException("Only a confirmed reservation can be checked in.");

        Status = "CheckedIn";
    }

    public void CheckOut()
    {
        if (Status != "CheckedIn")
            throw new InvalidOperationException("Only a checked-in reservation can be checked out.");

        Status = "CheckedOut";
    }

    public void Cancel()
    {
        if (Status != "Pending" && Status != "Confirmed")
            throw new InvalidOperationException("Only a pending or confirmed reservation can be cancelled.");

        Status = "Cancelled";
    }
}
