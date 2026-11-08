using Riok.Mapperly.Abstractions;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Users.Commands.RegisterUser;
using Travel_and_Accommodation_Booking_Platform_BackEnd.DTOs;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.MapperlyMappings;

[Mapper]
public partial class UserRegistrationRequestToRegisterUserCommandMapper
{ 
    public partial RegisterUserCommand MapToCommand(UserRegistrationRequest entity);
}