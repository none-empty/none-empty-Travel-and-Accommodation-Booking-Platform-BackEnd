using Microsoft.EntityFrameworkCore;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Repositories;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.StateEnums;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.AppDbContextFiles;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Repositories;

public class HotelRepository : IHotelRepository
{
    private readonly AppDbContext _context;

    public HotelRepository(AppDbContext context)
    {
        _context = context;
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
        return _context.Discounts
            .Where(discount => discount.HotelId.Equals(id) 
                               && discount.State == DiscountState.Active)
            .ToListAsync();
    }
}