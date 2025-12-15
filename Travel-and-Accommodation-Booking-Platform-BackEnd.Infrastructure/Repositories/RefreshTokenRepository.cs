using Microsoft.EntityFrameworkCore;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Repositories;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.RefreshTokensFiles;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.AppDbContextFiles;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDbContext _context;

    public RefreshTokenRepository(AppDbContext context)
    {
        _context = context;
    }
    
 
    public async Task AddAsync(RefreshToken entity)
    {
        _context.Set<RefreshToken>().Add(entity);

        await _context.SaveChangesAsync();
    }

 
    public async Task DeleteAsync(RefreshToken entity)
    {
        _context.Set<RefreshToken>().Remove(entity);
        await _context.SaveChangesAsync();
    }

 
    public async Task UpdateAsync(RefreshToken entity)
    {
        _context.Set<RefreshToken>().Update(entity); 
        await _context.SaveChangesAsync();
    }
    
    
    public async Task<RefreshToken?> GetByIdAsync(int id) => await _context.Set<RefreshToken>().FindAsync(id);
    
    public Task<RefreshToken?> ByTokenWithUser(string token)
    {
        return _context.Set<RefreshToken>()
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Token.Equals(token));
    }
}