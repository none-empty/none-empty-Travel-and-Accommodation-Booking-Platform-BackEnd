namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;

public interface ITokenBlacklistService
{
    Task BlacklistAsync(string jti, TimeSpan ttl);
    Task<bool> IsBlacklistedAsync(string jti);
}