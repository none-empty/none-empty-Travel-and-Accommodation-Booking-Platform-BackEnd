using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Search;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Controllers;

[ApiController]
[Route("api/v1/cities")]
[ResponseCache(Duration = 40, Location = ResponseCacheLocation.Client)]
[Authorize(Policy = "Admin Only")]
public class CitySearchController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public CitySearchController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Search([FromQuery]string search = "",[FromQuery]Guid prevElementId = default
        ,[FromQuery]int limit = 30,[FromQuery]string? sortBy = null,[FromQuery]bool down = true)
    {
        var query = new FetchNextRecordsQuery<City>(prevElementId,limit,
            search,down,sortBy);

        var result = await _mediator.Send(query);
        
        return StatusCode(StatusCodes.Status200OK,result);
    }
}