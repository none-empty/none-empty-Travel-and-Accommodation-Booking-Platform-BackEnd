using System.Linq.Expressions;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;

public interface ISearchPredicateRepo<T>
{
    public List<Expression<Func<T, bool>>> GetSearchPredicates(string search,Guid prevElementId,bool down);
    public  Expression<Func<T,Guid>>GetPrimaryKeyDelegate();
    public Expression<Func<T, string>>? GetSortByDelegate(string? sortBy);
}