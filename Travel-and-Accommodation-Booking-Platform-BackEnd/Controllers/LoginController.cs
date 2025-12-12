using MediatR;
using Microsoft.AspNetCore.Mvc;
using Travel_and_Accommodation_Booking_Platform_BackEnd.DTOs;
using Travel_and_Accommodation_Booking_Platform_BackEnd.MapperlyMappings;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Controllers;

[ApiController]
[Route("api/v1/login")]
public class LoginController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly UserLoginRequestToUserLoginCommandMapper _mapper;
    public LoginController(IMediator mediator, UserLoginRequestToUserLoginCommandMapper mapper )
    {
        _mediator = mediator;
        _mapper = mapper;
    }

     
    [HttpPost] 
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody]UserLoginRequest request)
    {
        var command = _mapper.MapToCommand(request);
        var result = await _mediator.Send(command);

        return StatusCode(StatusCodes.Status200OK, result);
    }
}