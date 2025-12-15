using MediatR;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Ad_Hoc_Persistance;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Repositories;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.StateEnums;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Reservations.Commands;

public class ConfirmReservationCommandHandler : 
    IRequestHandler<ConfirmReservationCommand,ConfirmReservationCommandResponse>
{
    private readonly IGuidGenerator _guidGenerator;
    private readonly IDateGetter _dateGetter;
    private readonly IConfirmReservation _confirmReservation;
    private readonly IHotelRepository _hotelRepository;

    public ConfirmReservationCommandHandler(IGuidGenerator guidGenerator,IDateGetter dateGetter
    ,IConfirmReservation confirmReservation,IHotelRepository hotelRepository)
    {
        _guidGenerator = guidGenerator;
        _dateGetter = dateGetter;
        _confirmReservation = confirmReservation;
        _hotelRepository = hotelRepository;
    }

    public async Task<ConfirmReservationCommandResponse> Handle(ConfirmReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = ConstructReservation(request);
        
        var roomsReservations = request.RoomsData
            .Select(roomData => 
                ConstructRoomReservation(roomData,reservation.ReservationId)
                )
            .ToList();

        var total = await CalculateTotal(reservation.HotelId,reservation.NumberOfRooms
        ,reservation.CheckInDate,reservation.CheckOutDate);

        reservation.TotalPrice = total;
          await _confirmReservation.Execute(reservation, roomsReservations);
           
          return new ConfirmReservationCommandResponse
              (reservation.ReservationId,total);
    }

    private Reservation ConstructReservation(ConfirmReservationCommand request)
    {
       return new Reservation
       {
           ReservationId = _guidGenerator.Generate(),
           CheckInDate = request.CheckInDate,
           CheckOutDate = request.CheckOutDate,
           Date = _dateGetter.GetUtcNow(),
           HotelId = request.HotelId,
           NumberOfRooms = request.RoomsData.Count,
           PaymentMethod = request.PaymentMethod,
           Remarks = request.Remarks,
           Status = BookingStatus.Scheduled,
           UserId = request.UserId
       };
    }
    
    private RoomReservation ConstructRoomReservation(RoomReservationInfo roomData,Guid reservationId)
    {
        return new RoomReservation
        {
            RoomReservationId = _guidGenerator.Generate(),
            RoomId = roomData.RoomId,
            NumberOfAdults = roomData.NumberOfAdults,
            NumberOfChildren = roomData.NumberOfChildren,
            ReservationId = reservationId,
        };
    }
    
    private async Task<decimal> CalculateTotal(Guid hotelId,int numberOfRooms,DateTime checkInDate
        ,DateTime checkOutDate)
    {
        var pricePerNight = (await _hotelRepository.GetHotelDiscounts(hotelId))
            .FirstOrDefault()?.DiscountedPrice;

        if (pricePerNight is null)
            pricePerNight = (await _hotelRepository.GetByIdAsync(hotelId))!.PricePerNight;

        var numberOfDays = (checkOutDate - checkInDate).TotalDays;

        var totalPrice = ((decimal)pricePerNight) * numberOfRooms * (decimal)numberOfDays;
        return totalPrice;
    }
}