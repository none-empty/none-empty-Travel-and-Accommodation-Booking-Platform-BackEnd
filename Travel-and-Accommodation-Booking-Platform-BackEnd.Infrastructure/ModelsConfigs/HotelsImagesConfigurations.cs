using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.ModelsConfigs;

public class HotelsImagesConfigurations: IEntityTypeConfiguration<HotelImage>
{
    public void Configure(EntityTypeBuilder<HotelImage> builder)
    {
        builder.HasKey(hotelImage => hotelImage.ImageId);
        
        builder.HasOne(hotelImage => hotelImage.Hotel)
            .WithMany()
            .HasForeignKey(hotelImage => hotelImage.HotelId);
    }
}