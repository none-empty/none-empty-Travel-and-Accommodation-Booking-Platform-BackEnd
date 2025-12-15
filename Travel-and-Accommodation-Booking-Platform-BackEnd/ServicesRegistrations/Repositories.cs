using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Repositories;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.RefreshTokensFiles;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Repositories;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.ServicesRegistrations;

public static class Repositories
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRepository<RefreshToken>, RefreshTokenRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        return services;
    }
}