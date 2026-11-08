using MediatR;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Users.Commands.Logout;

public record LogoutCommand(string Jti,string Exp) : IRequest;

 