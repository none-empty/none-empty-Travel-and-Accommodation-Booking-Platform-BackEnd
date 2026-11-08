using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Ad_Hoc_Persistance;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Controllers;

[ApiController]
[Route("api/v1/hotels")]
[Authorize]
[ResponseCache(Duration = 80, Location = ResponseCacheLocation.Any)]
public class FeaturedDealsController : ControllerBase
{
    private readonly IGetFeaturedDealsData _getFeaturedDealsData;
    public FeaturedDealsController(IGetFeaturedDealsData getFeaturedDealsData)
    {
        _getFeaturedDealsData = getFeaturedDealsData;
    }
    
    [HttpGet("featured-deals")] 
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetFeaturedDeals([FromQuery]int count = 5)
    {
        var result = await _getFeaturedDealsData.Execute(count);

        return StatusCode(StatusCodes.Status200OK, result);
    }
}