using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Migrations
{
    public partial class ReplaceRoomStatusWithIsBookable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(name: "IsBookable", table: "Rooms",
                type: "bit", nullable: false, defaultValue: false);
            migrationBuilder.Sql("UPDATE [Rooms] SET [IsBookable] = CASE WHEN [Status] = N'Unavailable' THEN 0 ELSE 1 END");
            migrationBuilder.DropColumn(name: "Status", table: "Rooms");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(name: "Status", table: "Rooms",
                type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: "Unavailable");
            migrationBuilder.Sql("UPDATE [Rooms] SET [Status] = CASE WHEN [IsBookable] = 1 THEN N'Available' ELSE N'Unavailable' END");
            migrationBuilder.DropColumn(name: "IsBookable", table: "Rooms");
        }
    }
}
