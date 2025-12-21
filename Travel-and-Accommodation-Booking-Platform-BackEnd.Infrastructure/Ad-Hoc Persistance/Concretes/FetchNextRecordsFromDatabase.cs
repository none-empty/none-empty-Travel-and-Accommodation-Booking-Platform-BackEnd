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
        List<Expression<Func<T5Entity,bool>>> searchPredicates,Expression<Func<T5Entity,string>>?getSortBy, 
        Expression<Func<T5Entity, Guid>>getPrimaryKey, int limit,bool down)
    {
        var baseQuery = _context.Set<T5Entity>();
        var query1 = baseQuery.Where(_ => false);
        foreach (var predicate in searchPredicates)
        {
            query1 = query1.Union(baseQuery.Where(predicate));
        }

        var query2 = down ? query1.OrderBy(getPrimaryKey): query1.OrderByDescending(getPrimaryKey);
        
        if(getSortBy is not null)
            query2 = down ? query2.ThenBy(getSortBy) : query2.ThenByDescending(getSortBy);
            
        var result = await query2.Take(limit + 1).ToListAsync();
        
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