using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.SearchPredicatesRepos;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.ServicesRegistrations;

public static class SearchPredicatesRepos
{
    public static IServiceCollection AddSearchPredicatesRepos(this IServiceCollection services)
    {
        services.AddSingleton<ISearchPredicateRepo<Hotel>, HotelSearchPredicatesRepo>();
        services.AddSingleton<ISearchPredicateRepo<Room>, RoomSearchPredicatesRepo>();
        services.AddSingleton<ISearchPredicateRepo<City>, CitySearchPredicatesRepo>();
        return services;
    }
}