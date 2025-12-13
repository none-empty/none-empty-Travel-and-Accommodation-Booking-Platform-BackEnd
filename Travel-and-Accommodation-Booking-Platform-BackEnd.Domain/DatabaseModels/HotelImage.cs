namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;

public class HotelImage
{
    public Guid ImageId { get; set; }
    public string ImageUrl { get; set; } = null!;
    public Guid HotelId { get; set; }
    
    public Hotel? Hotel { get; set; }
}