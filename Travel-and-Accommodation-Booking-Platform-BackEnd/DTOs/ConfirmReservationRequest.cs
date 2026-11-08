using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.StateEnums;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.DTOs;

public record ConfirmReservationRequest(Guid HotelId,DateTime CheckInDate,DateTime CheckOutDate
,PaymentMethod PaymentMethod,string? Remarks,List<RoomData>RoomsData);

public record RoomData(Guid RoomId,int NumberOfAdults,int NumberOfChildren);