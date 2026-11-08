using System.Security.Authentication;
using MediatR;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Repositories;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.RefreshTokensFiles;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Users.Commands.Login;

public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, UserLoginResponse>
{
    private readonly IRepository<RefreshToken> _refreshTokenRepo;
    private readonly IUserRepository _userRepo;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IPasswordHasher _hasher;
    private readonly IUserRoleRepository _userRoleRepository;

    public UserLoginCommandHandler(IRepository<RefreshToken>refreshTokenRepo,
        IUserRepository userRepo,ITokenGenerator tokenGenerator,IPasswordHasher hasher
        ,IUserRoleRepository userRoleRepository)
    {
        _refreshTokenRepo = refreshTokenRepo;
        _userRepo = userRepo;
        _tokenGenerator = tokenGenerator;
        _hasher = hasher;
        _userRoleRepository = userRoleRepository;
    }

    public async Task<UserLoginResponse> Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepo.GetBy(user => user.Email.Equals(request.Email));

        if (InvalidCredentials(user,request.Password)) throw new InvalidCredentialException("invalid credentials");

        var roles = await _userRoleRepository.GetUserRoles(user!.UserId);
        var accessToken = _tokenGenerator.GenerateToken(
            user.UserId,
            roles,
            user.Email,
            user.UserName
        );

        var refreshToken = _tokenGenerator.GenerateRefreshToken(user.UserId);
        await _refreshTokenRepo.AddAsync(refreshToken);
        return new UserLoginResponse(accessToken, refreshToken.Token);
    }

    private bool InvalidCredentials(User? user,string password)
    {
        return user is null || !_hasher.VerifyPassword(password,user.PasswordHash);
    }
}