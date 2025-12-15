using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Users.Commands.LoginUsingRefreshToken;
using Travel_and_Accommodation_Booking_Platform_BackEnd.DTOs;
using Travel_and_Accommodation_Booking_Platform_BackEnd.MapperlyMappings;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Controllers;

[ApiController]
[Route("api/v1/login-with-refresh-token")]
public class LoginWithRefreshTokenController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly LoginWithRefreshTokenRequestToLoginWithRefreshTokenCommandMapper _mapper;

    public LoginWithRefreshTokenController(IMediator mediator,
        LoginWithRefreshTokenRequestToLoginWithRefreshTokenCommandMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost] 
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody]LoginWithRefreshTokenRequest request)
    {

        var command = _mapper.MapToCommand(request);
        var result = await _mediator.Send(command);
        
        return StatusCode(StatusCodes.Status200OK,result);
    }
}