using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Ad_Hoc_Persistance;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Search;

public class FetchNextRoomsQueryHandler : FetchNextRecordsQueryHandler<Room>
{
    public FetchNextRoomsQueryHandler(ISearchPredicateRepo<Room> searchPredicateRepo, IFetchNextRecordsFromDatabase<Room> fetchNextRecordsFromDatabase) : base(searchPredicateRepo, fetchNextRecordsFromDatabase)
    {
    }
}