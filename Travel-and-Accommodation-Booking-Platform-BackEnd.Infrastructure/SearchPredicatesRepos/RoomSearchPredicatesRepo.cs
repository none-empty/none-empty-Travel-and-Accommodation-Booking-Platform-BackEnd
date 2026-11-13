using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.SearchPredicatesRepos;

public class RoomSearchPredicatesRepo : ISearchPredicateRepo<Room>
{
    public List<Expression<Func<Room, bool>>> GetSearchPredicates(string search)
    {
        if (string.IsNullOrWhiteSpace(search))
            return [entity => true];
        
        var predicates = new List<Expression<Func<Room, bool>>>()
        {
            room => EF.Functions.Like(room.Hotel!.HotelName, $"{search}" )
        };

        if (bool.TryParse(search, out var isBookable))
            predicates.Add(room => room.IsBookable == isBookable);

        if(int.TryParse(search,out int roomNumber))
            predicates.Add(room => room.RoomNumber == roomNumber);
        
        
        return predicates;
    }

    public Expression<Func<Room, bool>> GetCursorPredicate(Guid prevElementId, bool down)
    {
        if (prevElementId == Guid.Empty)
            return entity => true;

        return down ? room => room.RoomId > prevElementId
            : room => room.RoomId < prevElementId;
    }

    public Expression<Func<Room, Guid>> GetPrimaryKeyDelegate()
    {
        return room => room.RoomId;
    }
}
