namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Exceptions;

public sealed class ConflictException(string publicMessage) : Exception(publicMessage)
{
    public string PublicMessage { get; } = publicMessage;
}
