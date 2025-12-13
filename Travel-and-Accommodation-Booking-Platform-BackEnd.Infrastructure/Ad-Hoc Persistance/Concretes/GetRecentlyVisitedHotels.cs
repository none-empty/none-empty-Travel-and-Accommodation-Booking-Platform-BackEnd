using Microsoft.EntityFrameworkCore;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Ad_Hoc_Persistance;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Hotels.DTOs;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.AppDbContextFiles;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Ad_Hoc_Persistance.Concretes;

public class GetRecentlyVisitedHotels : IGetRecentlyVisitedHotels
{
    private readonly AppDbContext _context;
    private const int Count = 5;
    public GetRecentlyVisitedHotels(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<RecentlyVisitedHotelsResponse>> Execute(Guid userId)
    {
        return _context.Reservations
            .Include(r => r.Hotel)
            .ThenInclude(h => h!.City)
            .Where(r => r.UserId.Equals(userId) && r.CheckOutDate < DateTime.UtcNow)
            .OrderByDescending(r => r.CheckOutDate)
            .Take(Count)
            .Select(r => new RecentlyVisitedHotelsResponse(
                r.Hotel!.HotelName,
                r.Hotel.City!.Name,
                r.Hotel.StarRating,
                r.Hotel.ThumbnailUrl,
                r.Hotel.Category,
                r.Hotel.PricePerNight,
                r.Hotel.Description,
                r.Hotel.Address,
                r.Hotel.PhoneNumber
                ))
            .ToListAsync();


    }
}