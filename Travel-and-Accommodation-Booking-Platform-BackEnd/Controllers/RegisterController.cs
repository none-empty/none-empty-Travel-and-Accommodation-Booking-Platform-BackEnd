using MediatR;
using Microsoft.AspNetCore.Mvc;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Users.Commands.RegisterUser;
using Travel_and_Accommodation_Booking_Platform_BackEnd.DTOs;
using Travel_and_Accommodation_Booking_Platform_BackEnd.MapperlyMappings;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Controllers;

[ApiController]
[Route("api/v1/users")]
public class RegisterController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly UserRegistrationRequestToRegisterUserCommandMapper _mapper;
    public RegisterController(IMediator mediator,UserRegistrationRequestToRegisterUserCommandMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

     
    [HttpPost] 
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody]UserRegistrationRequest inputData)
    {
        var command = _mapper.MapToCommand(inputData);
        var result = await _mediator.Send(command);

        return StatusCode(StatusCodes.Status201Created, result);
    }
}