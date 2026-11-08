using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Services;

public class GuidGenerator : IGuidGenerator
{
    public Guid Generate() => Guid.NewGuid();
}