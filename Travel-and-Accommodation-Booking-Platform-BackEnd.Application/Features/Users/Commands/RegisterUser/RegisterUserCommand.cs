using MediatR;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Users.Commands.RegisterUser;

public record RegisterUserCommand(string UserName,string Password,string Email,string PhoneNumber) : IRequest<RegisterUserResponse>;

public record RegisterUserResponse(Guid UserId, string Email,string AccessToken,string RefreshToken);