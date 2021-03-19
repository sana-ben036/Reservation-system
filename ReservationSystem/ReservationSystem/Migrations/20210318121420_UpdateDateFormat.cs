using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReservationSystem.Migrations
{
    public partial class UpdateDateFormat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date_R",
                table: "Reservations",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Date_R",
                table: "Reservations",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(DateTime));
        }
    }
}
