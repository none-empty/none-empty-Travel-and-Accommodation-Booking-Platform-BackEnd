namespace Travel_and_Accommodation_Booking_Platform.DB.DatabaseModels;

public class Amenity
{
    public Guid AmenityId { get; set; }
    public string AmenityName { get; set; } = null!;


    public ICollection<HotelAmenity> HotelAmenities { get; set; } = new List<HotelAmenity>();
}
