namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;

public interface ITokenGenerator
{
    public string GenerateToken(Guid sub, string role, string email, string userName);
    public string GenerateRefreshToken();
}