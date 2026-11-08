using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Hotels.Queries;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Controllers;

[ApiController]
[Route("api/v1/hotels/{id}")]
[Authorize]
[ResponseCache(Duration = 20, Location = ResponseCacheLocation.Client)]
public class HotelInfoPageController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public HotelInfoPageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDetailedHotelInfo(Guid id)
    {
        await _mediator.Send(new GetHotelDetailedInfoQuery(id));   

        return StatusCode(StatusCodes.Status200OK);
    }
}