using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.StateEnums;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;

public class UserRole
{
    public Guid UserId { get; set; }
    public SiteRole Role { get; set; }
    
    public User? User { get; set; }
}