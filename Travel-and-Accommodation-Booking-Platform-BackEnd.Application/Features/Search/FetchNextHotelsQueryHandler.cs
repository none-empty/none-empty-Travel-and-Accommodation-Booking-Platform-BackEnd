using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Ad_Hoc_Persistance;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Search;

public class FetchNextHotelsQueryHandler : FetchNextRecordsQueryHandler<Hotel>
{
    public FetchNextHotelsQueryHandler(ISearchPredicateRepo<Hotel> searchPredicateRepo, IFetchNextRecordsFromDatabase<Hotel> fetchNextRecordsFromDatabase) : base(searchPredicateRepo, fetchNextRecordsFromDatabase)
    {
    }
}