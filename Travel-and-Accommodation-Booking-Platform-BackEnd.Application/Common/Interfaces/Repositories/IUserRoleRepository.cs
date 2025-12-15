using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.StateEnums;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Repositories;

public interface IUserRoleRepository
{
    Task<List<SiteRole>> GetUserRoles(Guid userId);
    Task Add(UserRole userRole);
    Task Remove(UserRole userRole);
}