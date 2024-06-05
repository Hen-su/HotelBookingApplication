using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBookingApplication.Migrations
{
    /// <inheritdoc />
    public partial class updatedReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ReservationPrice",
                table: "Reservations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationPrice",
                table: "Reservations");
        }
    }
}
