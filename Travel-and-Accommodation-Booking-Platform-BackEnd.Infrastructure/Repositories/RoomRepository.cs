using Microsoft.EntityFrameworkCore;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Repositories;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.AppDbContextFiles;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Repositories;

public class RoomRepository : IRepository<Room>
{
    private readonly AppDbContext _context;

    public RoomRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task AddAsync(Room entity)
    {
        _context.Set<Room>().Add(entity);
        return _context.SaveChangesAsync();
    }

    public Task DeleteAsync(Room entity)
    {
        _context.Set<Room>().Remove(entity);
        return _context.SaveChangesAsync();
    }

    public Task UpdateAsync(Room entity)
    {
        _context.Set<Room>().Update(entity);
        return _context.SaveChangesAsync();
    }

    public Task<Room?> GetByIdAsync(Guid id)
    {
        return _context.Set<Room>()
            .FirstOrDefaultAsync(room => room.RoomId.Equals(id));
    }

    public Task DeleteByIdAsync(Guid id)
    {
        return _context.Set<Room>()
            .Where(room => room.RoomId.Equals(id))
            .ExecuteDeleteAsync();
    }
}