using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Hotels.DTOs;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Ad_Hoc_Persistance;

public interface IGetRecentlyVisitedHotels
{
    Task<List<RecentlyVisitedHotelsResponse>> Execute(Guid userId);
}