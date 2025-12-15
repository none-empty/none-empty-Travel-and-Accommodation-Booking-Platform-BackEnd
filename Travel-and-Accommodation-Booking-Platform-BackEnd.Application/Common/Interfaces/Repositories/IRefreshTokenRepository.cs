using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.RefreshTokensFiles;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Repositories;

public interface IRefreshTokenRepository : IRepository<RefreshToken>
{
  Task<RefreshToken?> ByTokenWithUser(string token);
}