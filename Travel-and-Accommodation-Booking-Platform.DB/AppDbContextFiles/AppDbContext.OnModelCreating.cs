using Microsoft.EntityFrameworkCore;
using Travel_and_Accommodation_Booking_Platform.DB.DatabaseModels;

namespace Travel_and_Accommodation_Booking_Platform.DB.AppDbContextFiles;

public partial class AppDbContext
{
 protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);

        //
        // CITY
        //
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(city => city.CityId);
            entity.ToTable(t => 
                t.HasCheckConstraint("CK_City_NumberOfHotels_NonNegative", "[NumberOfHotels] >= 0")
            );
        });

        //
        // HOTEL
        //
        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(hotel => hotel.HotelId);

            entity.Property(hotel => hotel.HotelName).IsRequired();

            entity.HasOne(hotel => hotel.City)
                .WithMany(city => city.Hotels)
                .HasForeignKey(hotel => hotel.CityId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.Property(hotel => hotel.Category)
                .HasConversion<string>()
                .HasMaxLength(200);
            
            entity.Property(hotel => hotel.PricePerNight).HasColumnType("decimal(34,2)");
            
            entity.ToTable(t => 
                t.HasCheckConstraint("CK_Hotel_PricePerNight_NonNegative", "[PricePerNight] >= 0")
            );
            
            entity.ToTable(t => 
                t.HasCheckConstraint("CK_Hotel_StarRating_InRange", "[StarRating] BETWEEN 1 AND 5")
            );
            
            entity.ToTable(t => t.HasCheckConstraint(
                "CK_Hotel_PhoneNumber_ValidFormat",
                "[PhoneNumber] NOT LIKE '%[^0-9]%' AND LEN(PhoneNumber) BETWEEN 7 AND 15"
            ));
        });

        //
        // AMENITY
        //
        modelBuilder.Entity<Amenity>(entity =>
        {
            entity.HasKey(e => e.AmenityId);
 
        });

        //
        // HOTELAMENITY (many-to-many)
        //
        modelBuilder.Entity<HotelAmenity>(entity =>
        {
            entity.HasKey(e => new { e.HotelId, e.AmenityId });

            entity.HasOne(e => e.Hotel)
                .WithMany(h => h.HotelAmenities)
                .HasForeignKey(e => e.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Amenity)
                .WithMany(a => a.HotelAmenities)
                .HasForeignKey(e => e.AmenityId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        //
        // ROOM
        //
        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(room => room.RoomId);

            entity.Property(room => room.Status)
                .HasConversion<string>()
                .HasMaxLength(200);
            
            entity.ToTable(t => 
                t.HasCheckConstraint("CK_Room_RoomNumber_Positive", "[RoomNumber] > 0")
            );
            
            entity.ToTable(t => 
                t.HasCheckConstraint("CK_Room_AdultsCapacity_NonNegative", "[AdultsCapacity] >= 0")
            );
            
            entity.ToTable(t => 
                t.HasCheckConstraint("CK_Room_ChildrenCapacity_NonNegative", "[ChildrenCapacity] >= 0")
            );
            
            entity.HasOne(room => room.Hotel)
                .WithMany(hotel => hotel.Rooms)
                .HasForeignKey(room => room.HotelId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        //
        // DISCOUNT
        //
        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(discount => discount.DiscountId);

            entity.Property(discount => discount.State)
                .HasConversion<string>()
                .HasMaxLength(200);
            
            entity.Property(discount => discount.OriginalPrice).HasColumnType("decimal(34,2)");
            entity.Property(discount => discount.DiscountedPrice).HasColumnType("decimal(34,2)");
            
            entity.HasOne(discount => discount.Hotel)
                .WithMany(hotel => hotel.Discounts)
                .HasForeignKey(discount => discount.HotelId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        //
        // USER
        //
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(user => user.UserId);

            entity.ToTable(t => t.HasCheckConstraint(
                "CK_User_Email_ValidFormat",
                "[Email] LIKE '_%@_%._%'"
            ));
            
            entity.HasIndex(user => user.UserName)
                .IsUnique();
            
            entity.HasIndex(user => user.Email)
                .IsUnique();
            
            entity.ToTable(t => t.HasCheckConstraint(
                "CK_User_PhoneNumber_ValidFormat",
                "[PhoneNumber] NOT LIKE '%[^0-9]%' AND LEN(PhoneNumber) BETWEEN 7 AND 15"
            ));
        });

        //
        // RESERVATION
        //
        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(reservation => reservation.ReservationId);

            entity.Property(reservation => reservation.Status)
                .HasConversion<string>()
                .HasMaxLength(200);
            
            entity.Property(reservation => reservation.PaymentMethod)
                .HasConversion<string>()
                .HasMaxLength(200);
            
            entity.Property(reservation => reservation.TotalPrice).HasColumnType("decimal(34,2)");
            
            entity.ToTable(t => 
                t.HasCheckConstraint("CK_reservation_TotalPrice_NonNegative", "[TotalPrice] >= 0")
            );
            
            entity.ToTable(t => 
                t.HasCheckConstraint("CK_reservation_NumberOfRooms_Positive", "[NumberOfRooms] > 0")
            );
            
            entity.HasOne(reservation => reservation.User)
                .WithMany(user => user.Reservations)
                .HasForeignKey(reservation => reservation.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(reservation => reservation.Hotel)
                .WithMany(hotel => hotel.Reservations)
                .HasForeignKey(reservation => reservation.HotelId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        //
        // ROOMRESERVATION (Reservation <-> Room join table)
        //
        modelBuilder.Entity<RoomReservation>(entity =>
        {
            entity.HasKey(e => e.RoomReservationId);

            entity.HasOne(e => e.Room)
                .WithMany(r => r.RoomReservations)
                .HasForeignKey(e => e.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Reservation)
                .WithMany(r => r.RoomsReservations)
                .HasForeignKey(e => e.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}   
