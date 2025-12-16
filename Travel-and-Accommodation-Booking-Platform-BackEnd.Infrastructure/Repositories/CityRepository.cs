using Microsoft.EntityFrameworkCore;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Repositories;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.AppDbContextFiles;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Repositories;

public class CityRepository : IRepository<City>
{
    private readonly AppDbContext _context;

    public CityRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task AddAsync(City entity)
    {
        _context.Set<City>().Add(entity);
        return _context.SaveChangesAsync();
    }

    public Task DeleteAsync(City entity)
    {
        _context.Set<City>().Remove(entity);
        return _context.SaveChangesAsync();
    }

    public Task UpdateAsync(City entity){
        _context.Set<City>().Update(entity);
        return _context.SaveChangesAsync();
    }

    public async Task<City?> GetByIdAsync(Guid id) => await _context.Set<City>().FindAsync(id);

    public Task DeleteByIdAsync(Guid id)
    {
        return _context.Set<City>()
            .Where(city => city.CityId.Equals(id))
            .ExecuteDeleteAsync();
    }
    
}