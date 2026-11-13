namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Exceptions;

public sealed class ConflictException(string publicMessage,
    IReadOnlyList<RoomBookingConflict>? conflicts = null) : Exception(publicMessage)
{
    public string PublicMessage { get; } = publicMessage;
    public IReadOnlyList<RoomBookingConflict>? Conflicts { get; } = conflicts;
}

public record ReservationWindow(DateTime CheckInDate, DateTime CheckOutDate);
public record RoomBookingConflict(Guid RoomId, IReadOnlyList<ReservationWindow> Windows);
