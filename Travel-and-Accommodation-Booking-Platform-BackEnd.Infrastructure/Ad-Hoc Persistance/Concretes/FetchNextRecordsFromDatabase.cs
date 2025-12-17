using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Ad_Hoc_Persistance;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Search;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.AppDbContextFiles;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Ad_Hoc_Persistance.Concretes;

public class FetchNextRecordsFromDatabase<T5Entity> :IFetchNextRecordsFromDatabase<T5Entity>
where T5Entity : class
{
    private readonly AppDbContext _context;

    public FetchNextRecordsFromDatabase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<FetchNextRecordsQueryResponse<T5Entity>> Execute(
        List<Expression<Func<T5Entity,bool>>> searchPredicates, 
        Expression<Func<T5Entity, Guid>>getPrimaryKey, int limit,bool down)
    {
        var baseQuery = _context.Set<T5Entity>();
        var query = baseQuery.Where(_ => true);
        foreach (var predicate in searchPredicates)
        {
            query = query.Union(baseQuery.Where(predicate));
        }

        query = down ? query.OrderBy(getPrimaryKey) : query.OrderByDescending(getPrimaryKey);

        var result = await query.Take(limit + 1).ToListAsync();
        
        bool hasMore = result.Count > limit;
        var primaryKeyDelegate = getPrimaryKey.Compile();
        Guid? nextLastId = hasMore ? result.Select(entity =>primaryKeyDelegate(entity)).Last() : null;
        
        if (hasMore)
        {
            result.RemoveAt(result.Count - 1);
        }

        return new FetchNextRecordsQueryResponse<T5Entity>(result,nextLastId);
    }
}