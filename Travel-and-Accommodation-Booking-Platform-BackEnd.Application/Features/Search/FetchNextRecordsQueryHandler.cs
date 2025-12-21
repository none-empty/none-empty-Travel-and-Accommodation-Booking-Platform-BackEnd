using MediatR;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Ad_Hoc_Persistance;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Search;

public class FetchNextRecordsQueryHandler<T3Entity> : IRequestHandler<FetchNextRecordsQuery<T3Entity>,
    FetchNextRecordsQueryResponse<T3Entity>> where T3Entity : class
{
    private readonly ISearchPredicateRepo<T3Entity> _searchPredicateRepo;
    private readonly IFetchNextRecordsFromDatabase<T3Entity> _fetchNextRecordsFromDatabase;

    public FetchNextRecordsQueryHandler(ISearchPredicateRepo<T3Entity> searchPredicateRepo
    ,IFetchNextRecordsFromDatabase<T3Entity> fetchNextRecordsFromDatabase)
    {
        _searchPredicateRepo = searchPredicateRepo;
        _fetchNextRecordsFromDatabase = fetchNextRecordsFromDatabase;
    }

    public Task<FetchNextRecordsQueryResponse<T3Entity>> Handle(FetchNextRecordsQuery<T3Entity> request, CancellationToken cancellationToken)
    {
        var searchPredicates = _searchPredicateRepo.GetSearchPredicates(request.SearchTopic,
            request.PrevElementId,request.Down);
        var getPrimaryKey = _searchPredicateRepo.GetPrimaryKeyDelegate();
        var getSortBy = _searchPredicateRepo.GetSortByDelegate(request.SortBy);
        
        return _fetchNextRecordsFromDatabase.Execute(searchPredicates, getSortBy,getPrimaryKey,
            request.Limit, request.Down);
    }
}

 