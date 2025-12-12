using Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Jwt;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Settings;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.ServicesRegistrations;

public static class AppOptions
{
    public static IServiceCollection AddAppOptions(this IServiceCollection services,
        ConfigurationManager configuration)
    {
         services.Configure<JwtSettings>(
             configuration.GetSection(JwtSettings.SectionName));

         services.Configure<RefreshTokenSettings>(
             configuration.GetSection(RefreshTokenSettings.SectionName));
        return services;
    }
}