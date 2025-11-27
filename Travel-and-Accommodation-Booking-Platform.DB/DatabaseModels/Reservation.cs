using Travel_and_Accommodation_Booking_Platform.DB.StateEnums;

namespace Travel_and_Accommodation_Booking_Platform.DB.DatabaseModels;

public class Reservation
{
    public Guid ReservationId { get; set; }

    public Guid UserId { get; set; }      
    public Guid HotelId { get; set; }     

    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public BookingStatus Status { get; set; }
    public int NumberOfRooms { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime Date { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string? Remarks { get; set; }

    // Optional refs
    public User? User { get; set; }
    public Hotel? Hotel { get; set; }

     
    public ICollection<RoomReservation> RoomsReservations { get; set; }
}
 