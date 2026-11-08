using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Users.Commands.Logout;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Controllers;

[ApiController]
[Route("api/v1/logout")]
[Authorize]
public class LogoutController : ControllerBase
{
    private readonly IMediator _mediator;

    public LogoutController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost] 
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Logout()
    {
        var jti = User.FindFirstValue(JwtRegisteredClaimNames.Jti);
        var exp = User.FindFirstValue(JwtRegisteredClaimNames.Exp);
        
        if (jti == null || exp == null)
            return StatusCode(StatusCodes.Status400BadRequest);

        await _mediator.Send(new LogoutCommand(jti, exp));
        
        return StatusCode(StatusCodes.Status200OK);
    }
}