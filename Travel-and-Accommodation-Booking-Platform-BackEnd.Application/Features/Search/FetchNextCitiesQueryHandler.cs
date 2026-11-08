using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Ad_Hoc_Persistance;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Search;

public class FetchNextCitiesQueryHandler : FetchNextRecordsQueryHandler<City>
{
    public FetchNextCitiesQueryHandler(ISearchPredicateRepo<City> searchPredicateRepo, IFetchNextRecordsFromDatabase<City> fetchNextRecordsFromDatabase) : base(searchPredicateRepo, fetchNextRecordsFromDatabase)
    {
        
    }
}