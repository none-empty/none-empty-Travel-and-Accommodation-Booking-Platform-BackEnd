using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Reservations.Commands;
using Travel_and_Accommodation_Booking_Platform_BackEnd.DTOs;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Controllers;

[ApiController]
[Route("api/v1/users/me/Reservations")]
public class ReservationConfirmationController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReservationConfirmationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost] 
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ConfirmReservation([FromBody]ConfirmReservationRequest request)
    {
        var parsed = Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ,out Guid userId);
        if(!parsed)return Unauthorized("User ID claim is missing or incorrect.");
        
        var command = new ConfirmReservationCommand(
            request.HotelId, userId,
            request.CheckInDate, request.CheckOutDate,
            request.PaymentMethod,request.Remarks,
            request.RoomsData.Select(data => new RoomReservationInfo(data.RoomId,
                data.NumberOfAdults,data.NumberOfChildren)).ToList()
            );

        var result = await _mediator.Send(command);
        return StatusCode(StatusCodes.Status201Created,result);
    }
}