using Riok.Mapperly.Abstractions;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Users.Commands.Login;
using Travel_and_Accommodation_Booking_Platform_BackEnd.DTOs;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.MapperlyMappings;

[Mapper]
public partial class UserLoginRequestToUserLoginCommandMapper
{
    public partial UserLoginCommand MapToCommand(UserLoginRequest entity);   
}