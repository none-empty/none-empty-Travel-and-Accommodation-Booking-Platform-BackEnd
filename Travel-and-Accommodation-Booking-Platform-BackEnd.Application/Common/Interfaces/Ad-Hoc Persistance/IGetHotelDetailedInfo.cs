using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Hotels.Queries;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Ad_Hoc_Persistance;

public interface IGetHotelDetailedInfo
{
    Task<GetHotelDetailedInfoQueryResponse> Execute(Guid id);
}