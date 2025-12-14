namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;

public class HotelReview
{
    public Guid ReviewId { get;set; }
    public Guid HotelId { get; set; }
    public Guid UserId { get; set; }
    public string Review { get; set; } = null!;
    
    public Hotel? Hotel { get; set; }
    public User? User { get; set; }
}