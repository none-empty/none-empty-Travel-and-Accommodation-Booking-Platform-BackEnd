using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.SearchPredicatesRepos;

public class HotelSearchPredicatesRepo : ISearchPredicateRepo<Hotel>
{
    public List<Expression<Func<Hotel, bool>>> GetSearchPredicates(string search)
    {
        if (string.IsNullOrWhiteSpace(search))
            return [entity => true];
        return new List<Expression<Func<Hotel, bool>>>()
        {

          hotel => EF.Functions.Like(hotel.HotelName, $"{search}" ),
          hotel => EF.Functions.Like(hotel.Category.ToString(), $"{search}" ),
          hotel => EF.Functions.Like(hotel.Address, $"{search}" ),
          hotel => EF.Functions.Like(hotel.City!.Name, $"{search}" )
        };
    }

    public Expression<Func<Hotel, bool>> GetCursorPredicate(Guid prevElementId, bool down)
    {
        if (prevElementId == Guid.Empty)
            return entity => true;

        return down ? hotel => hotel.HotelId > prevElementId
            : hotel => hotel.HotelId < prevElementId;
    }

    public Expression<Func<Hotel, Guid>> GetPrimaryKeyDelegate()
    {
        return hotel => hotel.HotelId;
    }
}
