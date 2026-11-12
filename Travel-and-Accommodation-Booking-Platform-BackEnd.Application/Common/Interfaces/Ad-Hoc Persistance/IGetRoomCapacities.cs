namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Ad_Hoc_Persistance;

public interface IGetRoomCapacities
{
    Task<Dictionary<Guid, RoomCapacity>> Execute(IEnumerable<Guid> roomIds, CancellationToken cancellationToken);
}

public record RoomCapacity(int AdultsCapacity, int ChildrenCapacity);
