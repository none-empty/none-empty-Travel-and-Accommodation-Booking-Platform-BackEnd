using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.RefreshTokensFiles;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Ad_Hoc_Persistance;

public interface IInsertUserRegistrationData
{
    Task Execute(User user,RefreshToken token,UserRole userRole);
}