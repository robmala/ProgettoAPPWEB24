using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgettoAPPWEB24.Migrations
{
    /// <inheritdoc />
    public partial class modifiche : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auto_AspNetUsers_ProgettoAPPWEB24UserId",
                table: "Auto");

            migrationBuilder.DropIndex(
                name: "IX_Auto_ProgettoAPPWEB24UserId",
                table: "Auto");

            migrationBuilder.DropColumn(
                name: "ProgettoAPPWEB24UserId",
                table: "Auto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProgettoAPPWEB24UserId",
                table: "Auto",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Auto_ProgettoAPPWEB24UserId",
                table: "Auto",
                column: "ProgettoAPPWEB24UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auto_AspNetUsers_ProgettoAPPWEB24UserId",
                table: "Auto",
                column: "ProgettoAPPWEB24UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
