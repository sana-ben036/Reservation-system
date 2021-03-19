using Microsoft.EntityFrameworkCore.Migrations;

namespace ReservationSystem.Migrations
{
    public partial class UpdateTypeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ReservationTypes_ReservationTypeId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ReservationTypeId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ReservationTypeId",
                table: "Reservations");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TypeId",
                table: "Reservations",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ReservationTypes_TypeId",
                table: "Reservations",
                column: "TypeId",
                principalTable: "ReservationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ReservationTypes_TypeId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_TypeId",
                table: "Reservations");

            migrationBuilder.AddColumn<int>(
                name: "ReservationTypeId",
                table: "Reservations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservationTypeId",
                table: "Reservations",
                column: "ReservationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ReservationTypes_ReservationTypeId",
                table: "Reservations",
                column: "ReservationTypeId",
                principalTable: "ReservationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
