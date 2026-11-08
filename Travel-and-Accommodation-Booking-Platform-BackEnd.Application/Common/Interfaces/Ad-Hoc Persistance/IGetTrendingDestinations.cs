using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Cities;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Ad_Hoc_Persistance;

public interface IGetTrendingDestinations
{
    Task<List<TrendingDestinationsResponse>> Execute();
}