using Travel_and_Accommodation_Booking_Platform_BackEnd.MapperlyMappings;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.ServicesRegistrations;

public static class MapperlyMappings
{
    public static void AddMapperlyMappings(this IServiceCollection services)
    {
        services.AddSingleton<UserRegistrationRequestToRegisterUserCommandMapper>();
    }
}