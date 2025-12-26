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
    private readonly IEmailServiceManager _emailServiceManager;
    private readonly IUserRepository _userRepository;

    public ConfirmReservationCommandHandler(IGuidGenerator guidGenerator,IDateGetter dateGetter
    ,IConfirmReservation confirmReservation,IHotelRepository hotelRepository,
    IEmailServiceManager emailServiceManager,IUserRepository userRepository)
    {
        _guidGenerator = guidGenerator;
        _dateGetter = dateGetter;
        _confirmReservation = confirmReservation;
        _hotelRepository = hotelRepository;
        _emailServiceManager = emailServiceManager;
        _userRepository = userRepository;
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

          var user = await _userRepository.GetByIdAsync(request.UserId);
          var userEmail = user!.Email;
          
          var hotel = (await _hotelRepository.GetByIdAsync(request.HotelId))!;
          
          var subject = $"Reservation Confirmation - {user.UserName}- {hotel.HotelName} - {request.CheckInDate}";
          
          var body = ConstructMessageBody(reservation.ReservationId, hotel,
              request.CheckInDate, request.CheckOutDate, roomsReservations.Count,user.UserName,
              reservation.PaymentMethod.ToString(),total);
              
              
          await _emailServiceManager.SendEmail(userEmail,subject,body,html:true);
          
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
    
    private async Task<decimal> CalculateTotal(Guid hotelId ,int numberOfRooms,DateTime checkInDate
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

    private string ConstructMessageBody(Guid reservationId, Hotel hotel ,DateTime checkInDate
    ,DateTime checkOutDate ,int numOfRooms,string userName,string paymentMethod
    ,decimal total)
    {
        var body = $@"
<div style='font-family: Arial, sans-serif; line-height: 1.6; color: #333; max-width: 600px;'>
    <p>Dear <strong>{userName}</strong>,</p>

    <p>We are delighted to confirm your upcoming reservation. We look forward to welcoming you soon!</p>

    <p>Please find your reservation details and billing summary below. We recommend reviewing this information to ensure everything is correct.</p>

    <hr style='border: 0; border-top: 1px solid #eee;' />

    <h3 style='color: #2c3e50;'>Reservation Details</h3>
    <table style='width: 100%; border-collapse: collapse;'>
        <tr>
            <td style='padding: 5px 0;'><strong>Confirmation Number:</strong></td>
            <td>{reservationId}</td>
        </tr>
        <tr>
            <td style='padding: 5px 0;'><strong>Check-in:</strong></td>
            <td>{checkInDate}</td>
        </tr>
        <tr>
            <td style='padding: 5px 0;'><strong>Check-out:</strong></td>
            <td>{checkOutDate}</td>
        </tr>
        <tr>
            <td style='padding: 5px 0;'><strong>Hotel Category:</strong></td>
            <td>{hotel.Category}</td>
        </tr>
    </table>

    <h3 style='color: #2c3e50; margin-top: 20px;'>Billing & Payment Summary</h3>
    <table style='width: 100%; border-collapse: collapse;'>
        <tr>
            <td style='padding: 5px 0;'><strong>Total Number Of Rooms:</strong></td>
            <td>{numOfRooms}</td>
        </tr>
        <tr>
            <td style='padding: 5px 0;'><strong>Total:</strong></td>
            <td style='font-size: 1.1em; color: #27ae60;'><strong>{total}</strong></td>
        </tr>
        <tr>
            <td style='padding: 5px 0;'><strong>Payment Method:</strong></td>
            <td>{paymentMethod}</td>
        </tr>
    </table>

    <p style='margin-top: 30px;'>Safe travels, and we will see you on <strong>{checkInDate}</strong>!</p>

    <p>Best regards,<br />
    <strong>{hotel.HotelName} Staff</strong></p>
</div>";



       return body;
    }
}