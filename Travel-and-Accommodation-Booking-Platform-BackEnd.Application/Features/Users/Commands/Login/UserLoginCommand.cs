using MediatR;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Users.Commands.RegisterUser;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Users.Commands.Login;

public record UserLoginCommand(string Email,string Password) : IRequest<UserLoginResponse>;

public record UserLoginResponse(string AccessToken,string RefreshToken);