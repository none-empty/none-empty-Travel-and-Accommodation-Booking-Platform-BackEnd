using MediatR;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Users.Commands.Login;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Users.Commands.LoginUsingRefreshToken;

public record LoginWithRefreshTokenCommand(string OldRefreshToken,Guid UserId) : IRequest<LoginWithRefreshTokenResponse>;

public record LoginWithRefreshTokenResponse(string AccessToken, string RefreshToken);

 