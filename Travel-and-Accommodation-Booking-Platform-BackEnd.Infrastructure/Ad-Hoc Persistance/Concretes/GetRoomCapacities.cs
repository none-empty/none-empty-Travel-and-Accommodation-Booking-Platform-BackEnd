using Microsoft.EntityFrameworkCore;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Ad_Hoc_Persistance;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.AppDbContextFiles;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Ad_Hoc_Persistance.Concretes;

public class GetRoomCapacities : IGetRoomCapacities
{
    private readonly AppDbContext _context;

    public GetRoomCapacities(AppDbContext context)
    {
        _context = context;
    }

    public Task<Dictionary<Guid, RoomCapacity>> Execute(IEnumerable<Guid> roomIds, CancellationToken cancellationToken)
    {
        var ids = roomIds.Distinct().ToArray();
        return _context.Rooms
            .AsNoTracking()
            .Where(room => ids.Contains(room.RoomId))
            .Select(room => new { room.RoomId, room.AdultsCapacity, room.ChildrenCapacity })
            .ToDictionaryAsync(room => room.RoomId,
                room => new RoomCapacity(room.AdultsCapacity, room.ChildrenCapacity), cancellationToken);
    }
}
