using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.StateEnums;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Hotels.DTOs;

public record RecentlyVisitedHotelsResponse(string HotelName,string CityName,int StarRating,string? ThumbnailUrl,
    HotelCategory Category,decimal PricePerNight,string? Description,string Address,string PhoneNumber);