using Microsoft.EntityFrameworkCore;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Interfaces.Ad_Hoc_Persistance;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.StateEnums;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.AppDbContextFiles;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Ad_Hoc_Persistance.Concretes;

public class ConfirmReservation : IConfirmReservation
{
    private readonly AppDbContext _context;

    public ConfirmReservation(AppDbContext context)
    {
        _context = context;
    }

    public async Task Execute(Reservation reservation, List<RoomReservation> roomsReservations)
    {
        var roomIds = roomsReservations
            .Select(room => room.RoomId).OrderBy(id => id).ToList();
        
        var placeholders = string.Join(", ", roomIds.Select((_,i) => $"{{{i}}}"));
        var sql = $"SELECT * FROM Rooms WITH (UPDLOCK, ROWLOCK) WHERE RoomId IN ({placeholders})";

        var strategy = _context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () => 
        {
             
            using var transaction = await _context.Database.BeginTransactionAsync();
            try 
            {
                 
                var rooms = await _context.Rooms
                    .FromSqlRaw(sql, roomIds.Cast<object>().ToArray())
                    .ToListAsync();

                if (rooms.Any(r => r.Status != RoomStatus.Available))
                    throw new Exception("One of the selected rooms is not available.");

                
                foreach (var room in rooms) { room.Status = RoomStatus.In_Use; }
                _context.Reservations.Add(reservation);
                _context.RoomReservations.AddRange(roomsReservations);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch 
            {
                await transaction.RollbackAsync();
                throw;
            }
        });
         
        
       
    }
}