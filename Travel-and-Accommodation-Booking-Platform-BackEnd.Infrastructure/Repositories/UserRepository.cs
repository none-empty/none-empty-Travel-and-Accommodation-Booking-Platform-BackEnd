using Microsoft.EntityFrameworkCore;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Repositories;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.AppDbContextFiles;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    
 
    public async Task AddAsync(User entity)
    {
        await _context.Set<User>().AddAsync(entity);

        await _context.SaveChangesAsync();
    }

 
    public async Task DeleteAsync(User entity)
    {
        _context.Set<User>().Remove(entity);
        await _context.SaveChangesAsync();
    }

 
    public async Task UpdateAsync(User entity)
    {
        _context.Set<User>().Update(entity); 
        await _context.SaveChangesAsync();
    }
    
    
    public async Task<User?> GetByIdAsync(int id) => await _context.Set<User>().FindAsync(id);

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Set<User>()
            .AnyAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<bool> ExistsByUserNameAsync(string userName, CancellationToken cancellationToken)
    {
        return await _context.Set<User>()
            .AnyAsync(u => u.UserName == userName, cancellationToken);
    }
}