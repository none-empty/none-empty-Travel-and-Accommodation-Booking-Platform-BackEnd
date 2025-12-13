using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.StateEnums;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;

public class Discount
{
    public Guid DiscountId { get; set; }
    public Guid HotelId { get; set; }   

    public string? DiscountDescription { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal DiscountedPrice { get; set; }
    public DateTime StartingDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public DiscountState State { get; set; }

    // Optional navigation
    public Hotel? Hotel { get; set; }
}