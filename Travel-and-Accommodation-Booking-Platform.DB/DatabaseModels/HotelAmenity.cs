namespace Travel_and_Accommodation_Booking_Platform.DB.DatabaseModels;

public class HotelAmenity
{
    public Guid HotelId { get; set; }
    public Guid AmenityId { get; set; }

    // Optional navigations
    public Hotel? Hotel { get; set; }
    public Amenity? Amenity { get; set; }
}
