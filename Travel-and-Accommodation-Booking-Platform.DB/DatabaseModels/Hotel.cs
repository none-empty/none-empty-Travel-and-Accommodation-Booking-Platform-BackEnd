using Travel_and_Accommodation_Booking_Platform.DB.StateEnums;

namespace Travel_and_Accommodation_Booking_Platform.DB.DatabaseModels;

public class Hotel
{
    public Guid HotelId { get; set; }

    public string HotelName { get; set; } = null!;
    public Guid CityId { get; set; }
    public int StarRating { get; set; }
    public string Owner { get; set; } = null!;
    public string? ThumbnailUrl { get; set; }
    public HotelCategory Category { get; set; }
    public decimal PricePerNight { get; set; }
    public string? Description { get; set; }
    public string Address { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    
    // Navigation (optional reference)
    public City? City { get; set; }


    public ICollection<Room> Rooms { get; set; } = new List<Room>();
    public ICollection<HotelAmenity> HotelAmenities { get; set; } = new List<HotelAmenity>();
    public ICollection<Discount> Discounts { get; set; } = new List<Discount>();
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
