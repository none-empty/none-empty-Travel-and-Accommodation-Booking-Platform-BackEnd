using Microsoft.EntityFrameworkCore;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Ad_Hoc_Persistance;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Hotels.DTOs;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.AppDbContextFiles;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Ad_Hoc_Persistance.Concretes;

public class GetFeaturedDealsData : IGetFeaturedDealsData
{
    private readonly AppDbContext _context;
    private readonly IDateGetter _dateGetter;
    
    public GetFeaturedDealsData(AppDbContext context, IDateGetter dateGetter)
    {
        _context = context;
        _dateGetter = dateGetter;
    }

    public Task<List<FeaturedDealsResponse>> Execute(int count)
    {
        var now = _dateGetter.GetUtcNow();
        return _context.Discounts
            .Include(discount => discount.Hotel)
            .Where(discount => !discount.IsCancelled
                && discount.StartingDate <= now && now < discount.ExpirationDate)
            .OrderBy(discount => discount.DiscountedPrice)
            .ThenBy(discount => discount.DiscountId)
            .Take(count)
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