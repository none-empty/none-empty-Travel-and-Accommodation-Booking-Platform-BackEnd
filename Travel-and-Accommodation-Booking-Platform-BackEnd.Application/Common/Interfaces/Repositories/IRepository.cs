namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Repositories;

public interface IRepository<T> where T : class   
{   
      Task AddAsync(T entity);   
      Task DeleteAsync(T entity);   
      Task UpdateAsync(T entity);
      Task<T?>GetByIdAsync(int id);
}