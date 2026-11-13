using Microsoft.EntityFrameworkCore;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Exceptions;
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

                if (rooms.Count != roomIds.Count)
                    throw new BadRequestException("One or more selected rooms do not exist.");
                if (rooms.Any(r => r.HotelId != reservation.HotelId))
                    throw new BadRequestException("All selected rooms must belong to the reservation's hotel.");

                foreach (var requested in roomsReservations)
                {
                    var room = rooms.Single(r => r.RoomId == requested.RoomId);
                    if (requested.NumberOfAdults > room.AdultsCapacity ||
                        requested.NumberOfChildren > room.ChildrenCapacity)
                        throw new BadRequestException("The requested guests exceed a selected room's capacity.");
                }

                var overlaps = await _context.RoomReservations
                    .Where(rr => roomIds.Contains(rr.RoomId) &&
                        (rr.Reservation!.Status == BookingStatus.Scheduled ||
                         rr.Reservation.Status == BookingStatus.Checked_In) &&
                        rr.Reservation.CheckInDate < reservation.CheckOutDate &&
                        rr.Reservation.CheckOutDate > reservation.CheckInDate)
                    .Select(rr => new
                    {
                        rr.RoomId,
                        rr.Reservation!.CheckInDate,
                        rr.Reservation.CheckOutDate
                    })
                    .ToListAsync();

                var conflicts = overlaps.GroupBy(r => r.RoomId)
                    .OrderBy(group => group.Key)
                    .Select(group => new RoomBookingConflict(group.Key,
                        group.Select(r => new ReservationWindow(r.CheckInDate, r.CheckOutDate))
                            .Distinct().OrderBy(window => window.CheckInDate).ToArray()))
                    .ToArray();
                if (conflicts.Length > 0)
                    throw new ConflictException("Some selected rooms have conflicting reservations.", conflicts);
                if (rooms.Any(r => !r.IsBookable))
                    throw new ConflictException("One or more selected rooms are disabled for bookings.");

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
