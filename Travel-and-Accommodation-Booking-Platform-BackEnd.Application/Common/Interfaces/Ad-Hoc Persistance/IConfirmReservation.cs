using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Reservations.Commands;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Ad_Hoc_Persistance;

public interface IConfirmReservation
{
    Task Execute(Reservation reservation, List<RoomReservation> roomsReservations);
}