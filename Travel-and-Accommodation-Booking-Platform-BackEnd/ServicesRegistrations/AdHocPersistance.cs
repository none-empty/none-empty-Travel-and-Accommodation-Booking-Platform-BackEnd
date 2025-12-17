using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Ad_Hoc_Persistance;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Ad_Hoc_Persistance.Concretes;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.ServicesRegistrations;

public static class AdHocPersistance
{
    public static IServiceCollection AddAdHocPersistance(this IServiceCollection services)
    {
        services.AddScoped<IInsertUserRegistrationData, InsertUserRegistrationData>();
        services.AddScoped<IGetFeaturedDealsData, GetFeaturedDealsData>();
        services.AddScoped<IGetRecentlyVisitedHotels, GetRecentlyVisitedHotels>();
        services.AddScoped<IGetTrendingDestinations, GetTrendingDestinations>();
        services.AddScoped<IGetHotelDetailedInfo, GetHotelDetailedInfo>();
        services.AddScoped<IConfirmReservation, ConfirmReservation>();
        services.AddScoped(typeof(IFetchNextRecordsFromDatabase<>), typeof(FetchNextRecordsFromDatabase<>));
        return services;
    }
}