using MediatR;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Search;

public record FetchNextRecordsQuery<T1Entity>(Guid PrevElementId,int Limit,string SearchTopic,bool Down,
string? SortBy) : IRequest<FetchNextRecordsQueryResponse<T1Entity>> where T1Entity : class;

public record FetchNextRecordsQueryResponse<T2Entity>(List<T2Entity>Records,Guid? NextLastId) 
    where T2Entity : class;
 