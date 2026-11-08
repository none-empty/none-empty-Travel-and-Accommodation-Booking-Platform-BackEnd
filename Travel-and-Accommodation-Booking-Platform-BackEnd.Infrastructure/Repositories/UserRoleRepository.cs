using Microsoft.EntityFrameworkCore;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Repositories;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.StateEnums;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.AppDbContextFiles;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Repositories;

public class UserRoleRepository : IUserRoleRepository
{
    private readonly AppDbContext _context;

    public UserRoleRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<SiteRole>> GetUserRoles(Guid userId)
    {
        return _context.Set<UserRole>()
            .Where(userRole => userRole.UserId.Equals(userId))
            .Select(userRole => userRole.Role).ToListAsync();
    }

    public Task Add(UserRole userRole)
    {
        _context.Set<UserRole>().Add(userRole);
       return _context.SaveChangesAsync();
    }

    public Task Remove(UserRole userRole)
    {
        _context.Set<UserRole>().Remove(userRole);
        return _context.SaveChangesAsync();
    }
}