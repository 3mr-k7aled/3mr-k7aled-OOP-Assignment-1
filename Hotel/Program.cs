
using System;

var guest = new Guest(1, "Amr Ali", "01012345678");
var room = new Room(101, "Double", 500m);

var res = guest.MakeReservation(1, new DateTime(2026, 10, 10), new DateTime(2026, 10, 13), room);
res.Confirm();
res.CheckIn();

Console.WriteLine($"Status: {res.Status}");
Console.WriteLine($"Total: {res.TotalCost}");
Console.WriteLine($"Reservations: {guest.Reservations.Count}");

try
{
    guest.MakeReservation(2, new DateTime(2026, 10, 11), new DateTime(2026, 10, 12), room);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
