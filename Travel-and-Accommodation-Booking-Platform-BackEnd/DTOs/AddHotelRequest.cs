using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.StateEnums;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.DTOs;

public record AddHotelRequest(string HotelName, Guid CityId, int StarRating, string Owner,
 string? ThumbnailUrl, HotelCategory Category ,decimal PricePerNight, string? Description 
 ,string Address, string PhoneNumber);