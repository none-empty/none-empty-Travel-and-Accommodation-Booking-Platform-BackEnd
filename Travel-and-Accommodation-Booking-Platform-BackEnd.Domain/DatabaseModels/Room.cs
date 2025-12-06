using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.StateEnums;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;

public class Room
{
    public Guid RoomId { get; set; }

    public Guid HotelId { get; set; }   

    public int RoomNumber { get; set; }
    public int AdultsCapacity { get; set; }
    public int ChildrenCapacity { get; set; }
    public RoomStatus Status { get; set; }
    public string? Description { get; set; }

    // Optional navigation
    public Hotel? Hotel { get; set; }


    public ICollection<RoomReservation> RoomReservations { get; set; } = new List<RoomReservation>();
}