using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.StateEnums;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.DTOs;

public record UpdateRoomRequest(Guid RoomId, Guid HotelId, int RoomNumber, int AdultsCapacity, int ChildrenCapacity  
    ,RoomStatus Status, string? Description);