using MediatR;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.StateEnums;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Reservations.Commands;

public record ConfirmReservationCommand(Guid HotelId,Guid UserId,DateTime CheckInDate,DateTime CheckOutDate
    ,PaymentMethod PaymentMethod,string? Remarks,List<RoomReservationInfo>RoomsData) 
    :IRequest<ConfirmReservationCommandResponse>;

public record RoomReservationInfo(Guid RoomId, int NumberOfAdults, int NumberOfChildren);

public record ConfirmReservationCommandResponse(Guid ReservationId,decimal TotalPrice);

 