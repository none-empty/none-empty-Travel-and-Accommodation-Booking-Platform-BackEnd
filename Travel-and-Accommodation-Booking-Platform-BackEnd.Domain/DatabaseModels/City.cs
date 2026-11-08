namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;

public class City
{
    public Guid CityId { get; set; }

    public string Name { get; set; } = null!;
    public string? ThumbnailUrl { get; set; }
    public string Country { get; set; } = null!;
    public string PostOffice { get; set; } = null!;
    public int NumberOfHotels { get; set; }


    public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
}
