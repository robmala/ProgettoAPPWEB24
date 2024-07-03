using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgettoAPPWEB24.Migrations
{
    /// <inheritdoc />
    public partial class Modifiche : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParkId",
                table: "Lotti");

            migrationBuilder.DropColumn(
                name: "ParkId",
                table: "Biglietti");

            migrationBuilder.AddColumn<int>(
                name: "IdParcheggio",
                table: "Lotti",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdParcheggio",
                table: "Biglietti",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdParcheggio",
                table: "Lotti");

            migrationBuilder.DropColumn(
                name: "IdParcheggio",
                table: "Biglietti");

            migrationBuilder.AddColumn<string>(
                name: "ParkId",
                table: "Lotti",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParkId",
                table: "Biglietti",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
