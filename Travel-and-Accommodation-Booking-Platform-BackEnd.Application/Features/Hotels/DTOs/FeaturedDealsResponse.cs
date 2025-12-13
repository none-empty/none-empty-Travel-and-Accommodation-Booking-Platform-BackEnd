namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Hotels.DTOs;

public record FeaturedDealsResponse(string HotelName,string CityName,string Address,string? ThumbnailUrl,
    decimal OriginalPrice, decimal DiscountedPrice,string? DiscountDescription,int StarRating);