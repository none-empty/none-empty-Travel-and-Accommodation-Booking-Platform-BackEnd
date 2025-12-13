using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Hotels",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddCheckConstraint(
                name: "CK_User_PhoneNumber_ValidFormat",
                table: "Users",
                sql: "[PhoneNumber] NOT LIKE '%[^0-9]%' AND LEN(PhoneNumber) BETWEEN 7 AND 15");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Hotel_PhoneNumber_ValidFormat",
                table: "Hotels",
                sql: "[PhoneNumber] NOT LIKE '%[^0-9]%' AND LEN(PhoneNumber) BETWEEN 7 AND 15");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_User_PhoneNumber_ValidFormat",
                table: "Users");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Hotel_PhoneNumber_ValidFormat",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Hotels");
        }
    }
}
