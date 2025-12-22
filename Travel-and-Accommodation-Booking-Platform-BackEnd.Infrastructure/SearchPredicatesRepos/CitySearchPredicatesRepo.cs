using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.SearchPredicatesRepos;

public class CitySearchPredicatesRepo : ISearchPredicateRepo<City>
{
    public List<Expression<Func<City, bool>>> GetSearchPredicates(string search, Guid prevElementId, bool down)
    {
        Expression<Func<City, bool>> idPredicate = down ? city => city.CityId > prevElementId : city => city.CityId < prevElementId;  
        
        var predicates = new List<Expression<Func<City, bool>>>()
        {
            idPredicate,
            city => EF.Functions.Like(city.Country , $"{search}" ),
            city => EF.Functions.Like(city.Name, $"{search}" ),
            city => EF.Functions.Like(city.PostOffice, $"{search}" ),
        };
        
        if(int.TryParse(search,out int numOfHotels))
            predicates.Add(city => city.NumberOfHotels == numOfHotels);

        return predicates;
    }

    public Expression<Func<City, Guid>> GetPrimaryKeyDelegate()
    {
        return city => city.CityId;
    }

    public Expression<Func<City, string>>? GetSortByDelegate(string? sortBy)
    {
        if (sortBy is null) return null;

        if (sortBy.Equals("Name"))
            return city => city.Name;

        if (sortBy.Equals("Country"))
            return city => city.Country;
        
        if (sortBy.Equals("PostOffice"))
            return city => city.PostOffice;

        if (sortBy.Equals("NumberOfHotels"))
            return city => city.NumberOfHotels.ToString();
        
        throw new InvalidOperationException("invalid sort by");
    }
}