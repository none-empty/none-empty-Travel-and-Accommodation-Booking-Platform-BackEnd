using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Repositories;

public interface IHotelRepository : IRepository<Hotel>
{
    Task<List<Discount>> GetHotelDiscounts(Guid id);
}