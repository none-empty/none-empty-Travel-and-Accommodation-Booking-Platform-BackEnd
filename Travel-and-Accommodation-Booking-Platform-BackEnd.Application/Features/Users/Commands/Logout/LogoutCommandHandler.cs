using MediatR;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Users.Commands.Logout;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
{
    private readonly ITokenBlacklistService _tokenBlacklistService;

    public LogoutCommandHandler(ITokenBlacklistService tokenBlacklistService)
    {
        _tokenBlacklistService = tokenBlacklistService;
    }

    public Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var expTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(request.Exp));
        var ttl = expTime - DateTimeOffset.UtcNow;

        if (ttl > TimeSpan.Zero)
            return _tokenBlacklistService.BlacklistAsync(request.Jti, ttl);

        return Task.CompletedTask;
    }
}