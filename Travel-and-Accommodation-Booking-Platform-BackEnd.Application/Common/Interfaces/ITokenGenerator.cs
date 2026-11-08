using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.RefreshTokensFiles;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.StateEnums;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;

public interface ITokenGenerator
{
    public string GenerateToken(Guid sub,List<SiteRole>roles ,string email, string userName);
    public string GenerateRefreshTokenValue();
    public RefreshToken GenerateRefreshToken(Guid userId);
}