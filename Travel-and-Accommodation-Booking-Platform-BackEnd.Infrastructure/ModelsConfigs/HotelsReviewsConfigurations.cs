using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.ModelsConfigs;

public class HotelsReviewsConfigurations : IEntityTypeConfiguration<HotelReview>
{
    public void Configure(EntityTypeBuilder<HotelReview> builder)
    {
        builder.HasKey(hotelReview => hotelReview.ReviewId);
        
        builder.HasOne(hotelReview => hotelReview.Hotel)
            .WithMany()
            .HasForeignKey(hotelReview => hotelReview.HotelId);
        
        builder.HasOne(hotelReview => hotelReview.User)
            .WithMany()
            .HasForeignKey(hotelReview => hotelReview.UserId);
        
    }
}