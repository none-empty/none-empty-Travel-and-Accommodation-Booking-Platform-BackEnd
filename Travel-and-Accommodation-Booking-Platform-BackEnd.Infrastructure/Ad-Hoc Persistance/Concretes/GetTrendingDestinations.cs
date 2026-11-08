using Microsoft.EntityFrameworkCore;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Ad_Hoc_Persistance;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Features.Cities;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.AppDbContextFiles;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Ad_Hoc_Persistance.Concretes;

public class GetTrendingDestinations : IGetTrendingDestinations
{
    private readonly AppDbContext _context;
    private const int Count = 5;
    public GetTrendingDestinations(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<TrendingDestinationsResponse>> Execute()
    {
        return _context.Cities
            .Include(city => city.Hotels)
            .ThenInclude(hotel => hotel.Reservations)
            .Select(city => new
            {
                city.Name,
                city.ThumbnailUrl,
                numOfReservations = city.Hotels.SelectMany(hotel => hotel.Reservations).Count()
            })
            .OrderByDescending(c => c.numOfReservations)
            .Take(Count)
            .Select(c => new TrendingDestinationsResponse(
                c.Name,
                c.ThumbnailUrl
                ))
            .ToListAsync();
    }
}