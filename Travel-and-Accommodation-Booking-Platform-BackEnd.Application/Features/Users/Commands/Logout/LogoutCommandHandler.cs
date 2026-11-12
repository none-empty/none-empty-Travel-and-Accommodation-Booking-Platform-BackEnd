using MediatR;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Repositories;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Users.Commands.Logout;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
{
    private readonly ITokenBlacklistService _tokenBlacklistService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public LogoutCommandHandler(ITokenBlacklistService tokenBlacklistService,
        IRefreshTokenRepository refreshTokenRepository)
    {
        _tokenBlacklistService = tokenBlacklistService;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var expTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(request.Exp));
        var ttl = expTime - DateTimeOffset.UtcNow;

        await _refreshTokenRepository.RevokeAsync(request.RefreshToken, request.UserId, cancellationToken);

        if (ttl > TimeSpan.Zero)
            await _tokenBlacklistService.BlacklistAsync(request.Jti, ttl);
    }
}
