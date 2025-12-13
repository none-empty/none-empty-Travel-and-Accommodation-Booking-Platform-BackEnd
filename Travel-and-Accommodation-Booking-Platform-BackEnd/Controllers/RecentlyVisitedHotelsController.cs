using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Ad_Hoc_Persistance;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/users/me/recently-visited-hotels")]
public class RecentlyVisitedHotelsController : ControllerBase
{
    private readonly IGetRecentlyVisitedHotels _getRecentlyVisitedHotels;
    
    public RecentlyVisitedHotelsController(IGetRecentlyVisitedHotels getRecentlyVisitedHotels)
    {
        _getRecentlyVisitedHotels = getRecentlyVisitedHotels;
    }
    
    [HttpGet] 
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetFeaturedDeals()
    {
        var parsed = Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,out Guid userId);
        if (!parsed)return Unauthorized("User ID claim is missing or incorrect.");

        var result = await _getRecentlyVisitedHotels.Execute(userId);
        
        return StatusCode(StatusCodes.Status200OK,result);
    }
}