using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBookingApplication.Migrations
{
    /// <inheritdoc />
    public partial class updatedRoomStatusToResStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomStatuses_RoomStatusId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "RoomStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_RoomStatusId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomStatusId",
                table: "Rooms");

            migrationBuilder.AddColumn<int>(
                name: "ReservationStatusId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ReservationStatuses",
                columns: table => new
                {
                    ReservationStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationStatuses", x => x.ReservationStatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservationStatusId",
                table: "Reservations",
                column: "ReservationStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ReservationStatuses_ReservationStatusId",
                table: "Reservations",
                column: "ReservationStatusId",
                principalTable: "ReservationStatuses",
                principalColumn: "ReservationStatusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ReservationStatuses_ReservationStatusId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "ReservationStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ReservationStatusId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ReservationStatusId",
                table: "Reservations");

            migrationBuilder.AddColumn<int>(
                name: "RoomStatusId",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RoomStatuses",
                columns: table => new
                {
                    RoomStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomStatuses", x => x.RoomStatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomStatusId",
                table: "Rooms",
                column: "RoomStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomStatuses_RoomStatusId",
                table: "Rooms",
                column: "RoomStatusId",
                principalTable: "RoomStatuses",
                principalColumn: "RoomStatusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
