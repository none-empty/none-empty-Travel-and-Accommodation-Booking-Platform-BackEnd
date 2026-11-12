using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.SearchPredicatesRepos;

public class CitySearchPredicatesRepo : ISearchPredicateRepo<City>
{
    public List<Expression<Func<City, bool>>> GetSearchPredicates(string search)
    {
        if (string.IsNullOrWhiteSpace(search))
            return [entity => true];
        
        var predicates = new List<Expression<Func<City, bool>>>()
        {
            city => EF.Functions.Like(city.Country , $"{search}" ),
            city => EF.Functions.Like(city.Name, $"{search}" ),
            city => EF.Functions.Like(city.PostOffice, $"{search}" ),
        };
        
        if(int.TryParse(search,out int numOfHotels))
            predicates.Add(city => city.NumberOfHotels == numOfHotels);

        return predicates;
    }

    public Expression<Func<City, bool>> GetCursorPredicate(Guid prevElementId, bool down)
    {
        if (prevElementId == Guid.Empty)
            return entity => true;

        return down ? city => city.CityId > prevElementId
            : city => city.CityId < prevElementId;
    }

    public Expression<Func<City, Guid>> GetPrimaryKeyDelegate()
    {
        return city => city.CityId;
    }
}
