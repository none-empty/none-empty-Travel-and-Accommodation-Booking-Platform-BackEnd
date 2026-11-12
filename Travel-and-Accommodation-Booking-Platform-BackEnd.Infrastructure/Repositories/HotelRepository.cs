using Microsoft.EntityFrameworkCore;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Repositories;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.AppDbContextFiles;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Repositories;

public class HotelRepository : IHotelRepository
{
    private readonly AppDbContext _context;
    private readonly IDateGetter _dateGetter;

    public HotelRepository(AppDbContext context, IDateGetter dateGetter)
    {
        _context = context;
        _dateGetter = dateGetter;
    }

    public Task AddAsync(Hotel entity)
    {
        _context.Set<Hotel>().Add(entity);
        return _context.SaveChangesAsync();
    }

    public Task DeleteAsync(Hotel entity)
    {
        _context.Set<Hotel>().Remove(entity);
        return _context.SaveChangesAsync();
    }

    public Task UpdateAsync(Hotel entity)
    {
        _context.Set<Hotel>().Update(entity);
        return _context.SaveChangesAsync();
    }

    public Task<Hotel?> GetByIdAsync(Guid id)
    {
        return _context.Set<Hotel>()
            .FirstOrDefaultAsync(hotel => hotel.HotelId.Equals(id));
    }

    public Task DeleteByIdAsync(Guid id)
    {
       return _context.Set<Hotel>()
            .Where(hotel => hotel.HotelId.Equals(id))
            .ExecuteDeleteAsync();
    }

    public Task<List<Discount>> GetHotelDiscounts(Guid id)
    {
        var now = _dateGetter.GetUtcNow();
        return _context.Discounts
            .Where(discount => discount.HotelId == id
                && !discount.IsCancelled
                && discount.StartingDate <= now
                && now < discount.ExpirationDate)
            .OrderBy(discount => discount.DiscountedPrice)
            .ThenBy(discount => discount.DiscountId)
            .ToListAsync();
    }
}
