using Microsoft.EntityFrameworkCore;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.RefreshTokensFiles;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.AppDbContextFiles;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<City> Cities { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Amenity> Amenities { get; set; }
    public DbSet<HotelAmenity> HotelAmenities { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<RoomReservation> RoomReservations { get; set; }
    public DbSet<RefreshToken> RefreshTokens;
    public DbSet<HotelImage> HotelsImages;
}