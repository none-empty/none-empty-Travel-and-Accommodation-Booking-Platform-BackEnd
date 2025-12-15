using MediatR;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Repositories;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.RefreshTokensFiles;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Users.Commands.LoginUsingRefreshToken;

public class LoginWithRefreshTokenCommandHandler : IRequestHandler<LoginWithRefreshTokenCommand, 
    LoginWithRefreshTokenResponse>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IUserRoleRepository _userRoleRepository;

    public LoginWithRefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository,
        ITokenGenerator tokenGenerator,IUserRoleRepository userRoleRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _tokenGenerator = tokenGenerator;
        _userRoleRepository = userRoleRepository;
    }

    public async Task<LoginWithRefreshTokenResponse> Handle(LoginWithRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await _refreshTokenRepository.ByTokenWithUser(request.OldRefreshToken);
        

        if (TokenIsNotValid(refreshToken))
            throw new ApplicationException("The refresh token has expired");


        if (TokenIsNotForThatUser(refreshToken!.UserId, request.UserId))
            throw new InvalidOperationException("mismatch between user and refresh token");

        var user = refreshToken.User;
        var roles = await _userRoleRepository.GetUserRoles(user!.UserId);
        string accessToken = _tokenGenerator.GenerateToken(
            user.UserId,
            roles,
            user.Email,
            user.UserName
            );

        refreshToken.Token = _tokenGenerator.GenerateRefreshTokenValue();
        refreshToken.ExpiresOnUtc = DateTime.UtcNow.AddDays(7);

        await _refreshTokenRepository.UpdateAsync(refreshToken);

        return new LoginWithRefreshTokenResponse(accessToken, refreshToken.Token);
    }

    private bool TokenIsNotForThatUser(Guid userId, Guid requestUserId)
    {
        return userId != requestUserId;
    }


    private bool TokenIsNotValid(RefreshToken? refreshToken)
    {
        return refreshToken is null || refreshToken.ExpiresOnUtc < DateTime.UtcNow;
    }
}