using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Ad_Hoc_Persistance;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.RefreshTokensFiles;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.AppDbContextFiles;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Ad_Hoc_Persistance.Concretes;

public class InsertUserRegistrationData : IInsertUserRegistrationData
{
    private readonly AppDbContext _context;
    
    public InsertUserRegistrationData(AppDbContext context)
    {
        _context = context;
    }
    public Task Execute(User user, RefreshToken token)
    {
        _context.Set<User>().Add(user);
        _context.Set<RefreshToken>().Add(token);
       return _context.SaveChangesAsync();
    }
}