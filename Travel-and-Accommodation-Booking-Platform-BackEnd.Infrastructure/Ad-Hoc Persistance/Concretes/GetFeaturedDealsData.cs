using Microsoft.EntityFrameworkCore;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Ad_Hoc_Persistance;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Hotels.DTOs;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.StateEnums;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.AppDbContextFiles;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Ad_Hoc_Persistance.Concretes;

public class GetFeaturedDealsData : IGetFeaturedDealsData
{
    private readonly AppDbContext _context;
    
    public GetFeaturedDealsData(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<FeaturedDealsResponse>> Execute(int count)
    {
        return _context.Discounts
            .Take(count)
            .Include(discount => discount.Hotel)
            .Where(discount => discount.State == DiscountState.Active)
            .Select(discount => new FeaturedDealsResponse(
                discount.Hotel!.HotelName,
                discount.Hotel.City!.Name,
                discount.Hotel.Address,
                discount.Hotel!.ThumbnailUrl,
                discount.OriginalPrice,
                discount.DiscountedPrice,
                discount.DiscountDescription,
                discount.Hotel.StarRating
                )).ToListAsync();
    }
}