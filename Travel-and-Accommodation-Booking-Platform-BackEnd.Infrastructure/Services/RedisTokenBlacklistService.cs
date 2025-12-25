using StackExchange.Redis;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Services;

public class RedisTokenBlacklistService : ITokenBlacklistService
{
    private readonly IDatabase _db;
    public RedisTokenBlacklistService(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
    }

    public Task BlacklistAsync(string jti, TimeSpan ttl)
    {
        return _db.StringSetAsync(
            $"blacklist:{jti}",
            "1",
            ttl);
    }

    public Task<bool> IsBlacklistedAsync(string jti)
    {
        return _db.KeyExistsAsync($"blacklist:{jti}");
    }
}