using MediatR;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Ad_Hoc_Persistance;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Repositories;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.StateEnums;


namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
{
    private readonly IGuidGenerator _guidGenerator;
    private readonly IPasswordHasher _hasher;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IInsertUserRegistrationData _insertUserRegistrationData;
    
    public RegisterUserCommandHandler(IGuidGenerator guidGenerator
        ,IPasswordHasher hasher,ITokenGenerator tokenGenerator, 
        IInsertUserRegistrationData insertUserRegistrationData)
    {
        _guidGenerator = guidGenerator;
        _hasher = hasher;
        _tokenGenerator = tokenGenerator;
        _insertUserRegistrationData = insertUserRegistrationData;
    }

    public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {

        var newUser = new User
        {
            UserId = _guidGenerator.Generate(),
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            UserName = request.UserName,
            PasswordHash = _hasher.HashPassword(request.Password)
        };

         
        var accessToken = _tokenGenerator.GenerateToken(
            newUser.UserId, 
            new List<SiteRole>() { SiteRole.User },
            newUser.Email,
            newUser.UserName
            );

        var refreshToken = _tokenGenerator.GenerateRefreshToken(newUser.UserId);

        await _insertUserRegistrationData.Execute(newUser, refreshToken,
            new UserRole{UserId = newUser.UserId,Role = SiteRole.User});
        
        return new RegisterUserResponse(newUser.UserId,newUser.Email,accessToken,refreshToken.Token);
    }
}