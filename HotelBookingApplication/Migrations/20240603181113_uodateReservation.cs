using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBookingApplication.Migrations
{
    /// <inheritdoc />
    public partial class uodateReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientName",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "NumberOfRooms",
                table: "Reservations",
                newName: "GuestDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_GuestDetailsId",
                table: "Reservations",
                column: "GuestDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_GuestDetails_GuestDetailsId",
                table: "Reservations",
                column: "GuestDetailsId",
                principalTable: "GuestDetails",
                principalColumn: "GuestDetailsId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_GuestDetails_GuestDetailsId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_GuestDetailsId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "GuestDetailsId",
                table: "Reservations",
                newName: "NumberOfRooms");

            migrationBuilder.AddColumn<string>(
                name: "ClientName",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
