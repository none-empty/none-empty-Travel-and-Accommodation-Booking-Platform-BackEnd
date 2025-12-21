using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Search;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Ad_Hoc_Persistance.Concretes;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Controllers;

[ApiController]
[Route("api/v1/hotels")]
[ResponseCache(Duration = 40, Location = ResponseCacheLocation.Client)]
[Authorize]
public class HotelSearchController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public HotelSearchController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Search([FromQuery]string search = "",[FromQuery]Guid prevElementId = default
    ,[FromQuery]int limit = 30,[FromQuery]string? sortBy = null,[FromQuery]bool down = true)
    {
        var query = new FetchNextRecordsQuery<Hotel>(prevElementId,limit,
            search,down,sortBy);

        var result = await _mediator.Send(query);
        
        return StatusCode(StatusCodes.Status200OK,result);
    }
}