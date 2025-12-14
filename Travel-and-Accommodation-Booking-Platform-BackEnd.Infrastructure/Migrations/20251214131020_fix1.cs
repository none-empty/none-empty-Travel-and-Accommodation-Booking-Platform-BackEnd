using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fix1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelImage_Hotels_HotelId",
                table: "HotelImage");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelReview_Hotels_HotelId",
                table: "HotelReview");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelReview_Users_UserId",
                table: "HotelReview");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_Users_UserId",
                table: "RefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelReview",
                table: "HotelReview");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelImage",
                table: "HotelImage");

            migrationBuilder.RenameTable(
                name: "RefreshToken",
                newName: "RefreshTokens");

            migrationBuilder.RenameTable(
                name: "HotelReview",
                newName: "HotelsReviews");

            migrationBuilder.RenameTable(
                name: "HotelImage",
                newName: "HotelsImages");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshToken_Token",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_Token");

            migrationBuilder.RenameIndex(
                name: "IX_HotelReview_UserId",
                table: "HotelsReviews",
                newName: "IX_HotelsReviews_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelReview_HotelId",
                table: "HotelsReviews",
                newName: "IX_HotelsReviews_HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelImage_HotelId",
                table: "HotelsImages",
                newName: "IX_HotelsImages_HotelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelsReviews",
                table: "HotelsReviews",
                column: "ReviewId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelsImages",
                table: "HotelsImages",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelsImages_Hotels_HotelId",
                table: "HotelsImages",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "HotelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelsReviews_Hotels_HotelId",
                table: "HotelsReviews",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "HotelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelsReviews_Users_UserId",
                table: "HotelsReviews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Users_UserId",
                table: "RefreshTokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelsImages_Hotels_HotelId",
                table: "HotelsImages");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelsReviews_Hotels_HotelId",
                table: "HotelsReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelsReviews_Users_UserId",
                table: "HotelsReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Users_UserId",
                table: "RefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelsReviews",
                table: "HotelsReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelsImages",
                table: "HotelsImages");

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                newName: "RefreshToken");

            migrationBuilder.RenameTable(
                name: "HotelsReviews",
                newName: "HotelReview");

            migrationBuilder.RenameTable(
                name: "HotelsImages",
                newName: "HotelImage");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshToken",
                newName: "IX_RefreshToken_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshToken",
                newName: "IX_RefreshToken_Token");

            migrationBuilder.RenameIndex(
                name: "IX_HotelsReviews_UserId",
                table: "HotelReview",
                newName: "IX_HotelReview_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelsReviews_HotelId",
                table: "HotelReview",
                newName: "IX_HotelReview_HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelsImages_HotelId",
                table: "HotelImage",
                newName: "IX_HotelImage_HotelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelReview",
                table: "HotelReview",
                column: "ReviewId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelImage",
                table: "HotelImage",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelImage_Hotels_HotelId",
                table: "HotelImage",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "HotelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelReview_Hotels_HotelId",
                table: "HotelReview",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "HotelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelReview_Users_UserId",
                table: "HotelReview",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_Users_UserId",
                table: "RefreshToken",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
