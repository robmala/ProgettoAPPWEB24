using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgettoAPPWEB24.Migrations
{
    /// <inheritdoc />
    public partial class _ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isRecharging",
                table: "Auto",
                newName: "IsRecharging");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsRecharging",
                table: "Auto",
                newName: "isRecharging");
        }
    }
}
