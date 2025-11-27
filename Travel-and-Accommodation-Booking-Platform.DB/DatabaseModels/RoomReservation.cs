namespace Travel_and_Accommodation_Booking_Platform.DB.DatabaseModels;

public class RoomReservation
{
    public Guid RoomReservationId { get; set; }

    public Guid RoomId { get; set; }
    public Guid ReservationId { get; set; }

    public int NumberOfAdults { get; set; }
    public int NumberOfChildren { get; set; }

    // Optional refs
    public Room? Room { get; set; }
    public Reservation? Reservation { get; set; }
}