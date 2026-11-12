using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Migrations
{
    public partial class ReplaceDiscountStateWithIsCancelled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "State", table: "Discounts");
            migrationBuilder.AddColumn<bool>(
                name: "IsCancelled",
                table: "Discounts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "IsCancelled", table: "Discounts");
            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Discounts",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "Pending");
        }
    }
}
