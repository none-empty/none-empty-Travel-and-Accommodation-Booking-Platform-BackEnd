using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.SearchPredicatesRepos;

public class HotelSearchPredicatesRepo : ISearchPredicateRepo<Hotel>
{
    public List<Expression<Func<Hotel, bool>>> GetSearchPredicates(string search, Guid prevElementId,bool down)
    {
        Expression<Func<Hotel, bool>> idPredicate = down ? hotel => hotel.HotelId > prevElementId : hotel => hotel.HotelId < prevElementId;
        return new List<Expression<Func<Hotel, bool>>>()
        {
         idPredicate,
          hotel => EF.Functions.Like(hotel.HotelName, $"{search}" ),
          hotel => EF.Functions.Like(hotel.Category.ToString(), $"{search}" ),
          hotel => EF.Functions.Like(hotel.Address, $"{search}" ),
          hotel => EF.Functions.Like(hotel.City!.Name, $"{search}" )
        };
    }

    public Expression<Func<Hotel, Guid>> GetPrimaryKeyDelegate()
    {
        return hotel => hotel.HotelId;
    }

    public Expression<Func<Hotel, string>>? GetSortByDelegate(string? sortBy)
    {
        if (sortBy is null) return null;

        if (sortBy.Equals("HotelName"))
            return hotel => hotel.HotelName;

        if (sortBy.Equals("Category"))
            return hotel => hotel.Category.ToString();

        if (sortBy.Equals("Address"))
            return hotel => hotel.Address;

        if (sortBy.Equals("PhoneNumber"))
            return hotel => hotel.PhoneNumber;
        throw new InvalidOperationException("invalid sort by");
    }
}