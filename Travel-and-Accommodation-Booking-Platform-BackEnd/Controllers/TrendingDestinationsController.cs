using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Ad_Hoc_Persistance;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Controllers;


[ApiController]
[Route("api/v1/Cities/trending-destination")]
[Authorize]
public class TrendingDestinationsController : ControllerBase
{
    private readonly IGetTrendingDestinations _getTrendingDestinations;
    public TrendingDestinationsController(IGetTrendingDestinations getTrendingDestinations)
    {
        _getTrendingDestinations = getTrendingDestinations;
    }

    [HttpGet] 
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTrendingDestinations()
    {
        var result = await _getTrendingDestinations.Execute();

        return StatusCode(StatusCodes.Status200OK,result);
    } 
}