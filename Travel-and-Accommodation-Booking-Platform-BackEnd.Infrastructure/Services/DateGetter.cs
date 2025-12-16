using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Repositories;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Services;

public class DateGetter : IDateGetter
{
    public DateTime GetUtcNow()
    {
        return DateTime.UtcNow;
    }
}