using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Domain.DatabaseModels;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.ModelsConfigs;

public class UsersRolesConfigurations : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.Property(userRole => userRole.Role)
            .HasConversion<string>()
            .HasMaxLength(200);  
        
        builder.HasKey(userRole => new{userRole.UserId,userRole.Role});


        builder.HasOne(userRole => userRole.User)
            .WithMany()
            .HasForeignKey(userRole => userRole.UserId);
        
    }
}