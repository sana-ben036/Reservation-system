using Microsoft.EntityFrameworkCore.Migrations;

namespace ReservationSystem.Migrations
{
    public partial class spFullType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"CREATE PROCEDURE spFullType2 (IN t CHAR(255)) BEGIN SELECT count(*) FROM reservations WHERE TypeId = t; END";
            migrationBuilder.Sql(procedure);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"DROP PROCEDURE spFullType2 ";
            migrationBuilder.Sql(procedure);
        }
    }
}
