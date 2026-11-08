using System.Linq.Expressions;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);
    Task<bool> ExistsByUserNameAsync(string phoneNumber, CancellationToken cancellationToken);
    Task<User?> GetBy(Expression<Func<User,bool>> predicate);
}