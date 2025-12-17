using System.Linq.Expressions;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Search;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Ad_Hoc_Persistance;

public interface IFetchNextRecordsFromDatabase <T4Entity> where T4Entity : class
{
    Task<FetchNextRecordsQueryResponse<T4Entity>> Execute(
        List<Expression<Func<T4Entity,bool>>>searchPredicates,
        Expression<Func<T4Entity, Guid>> getPrimaryKey,int limit,bool down);
}