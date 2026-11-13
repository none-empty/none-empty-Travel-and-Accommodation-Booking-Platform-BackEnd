
namespace Travel_and_Accommodation_Booking_Platform_BackEnd.DTOs;

public record AddRoomRequest(Guid HotelId, int RoomNumber, int AdultsCapacity, int ChildrenCapacity  
 ,bool IsBookable, string? Description);