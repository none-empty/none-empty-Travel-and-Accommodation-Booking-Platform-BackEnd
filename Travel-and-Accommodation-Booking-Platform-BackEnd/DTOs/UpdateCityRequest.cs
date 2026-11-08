namespace Travel_and_Accommodation_Booking_Platform_BackEnd.DTOs;

public record UpdateCityRequest(Guid CityId,string Name,string? ThumbnailUrl,string Country,string PostOffice,
    int NumberOfHotels);