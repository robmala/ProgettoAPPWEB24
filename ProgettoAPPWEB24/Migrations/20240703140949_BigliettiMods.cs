using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgettoAPPWEB24.Migrations
{
    /// <inheritdoc />
    public partial class BigliettiMods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pagamenti_Biglietti_BigliettoId",
                table: "Pagamenti");

            migrationBuilder.DropIndex(
                name: "IX_Pagamenti_BigliettoId",
                table: "Pagamenti");

            migrationBuilder.RenameColumn(
                name: "BigliettoId",
                table: "Pagamenti",
                newName: "Ricarica");

            migrationBuilder.AddColumn<int>(
                name: "IdParcheggio",
                table: "Pagamenti",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Ingresso",
                table: "Pagamenti",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdParcheggio",
                table: "Pagamenti");

            migrationBuilder.DropColumn(
                name: "Ingresso",
                table: "Pagamenti");

            migrationBuilder.RenameColumn(
                name: "Ricarica",
                table: "Pagamenti",
                newName: "BigliettoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamenti_BigliettoId",
                table: "Pagamenti",
                column: "BigliettoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagamenti_Biglietti_BigliettoId",
                table: "Pagamenti",
                column: "BigliettoId",
                principalTable: "Biglietti",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
