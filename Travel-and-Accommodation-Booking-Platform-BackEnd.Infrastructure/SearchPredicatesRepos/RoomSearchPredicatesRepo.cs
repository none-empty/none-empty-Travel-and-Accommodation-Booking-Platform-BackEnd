using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.SearchPredicatesRepos;

public class RoomSearchPredicatesRepo : ISearchPredicateRepo<Room>
{
    public List<Expression<Func<Room, bool>>> GetSearchPredicates(string search, Guid prevElementId, bool down)
    {
        Expression<Func<Room, bool>> idPredicate = down ? room => room.RoomId > prevElementId : room => room.RoomId < prevElementId;
        
        var predicates = new List<Expression<Func<Room, bool>>>()
        {
            idPredicate,
            room => EF.Functions.Like(room.Hotel!.HotelName, $"{search}" ),
            room => EF.Functions.Like(room.Status.ToString(), $"{search}" )
        };

        if(int.TryParse(search,out int roomNumber))
            predicates.Add(room => room.RoomNumber == roomNumber);
        
        
        return predicates;
    }

    public Expression<Func<Room, Guid>> GetPrimaryKeyDelegate()
    {
        return room => room.RoomId;
    }

    public Expression<Func<Room, string>>? GetSortByDelegate(string? sortBy)
    {
        if (sortBy is null) return null;

        if (sortBy.Equals("RoomNumber"))
            return room => room.RoomNumber.ToString();

        if (sortBy.Equals("Status"))
            return room => room.Status.ToString();
        
        throw new InvalidOperationException("invalid sort by");
    }
}