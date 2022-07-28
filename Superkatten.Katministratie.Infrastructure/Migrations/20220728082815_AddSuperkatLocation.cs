using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Superkatten.Katministratie.Infrastructure.Migrations
{
    public partial class AddSuperkatLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CatchLocation",
                table: "SuperKatten");

            migrationBuilder.AddColumn<Guid>(
                name: "CatchLocationId",
                table: "SuperKatten",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SuperKatten_CatchLocationId",
                table: "SuperKatten",
                column: "CatchLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_SuperKatten_Locations_CatchLocationId",
                table: "SuperKatten",
                column: "CatchLocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SuperKatten_Locations_CatchLocationId",
                table: "SuperKatten");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_SuperKatten_CatchLocationId",
                table: "SuperKatten");

            migrationBuilder.DropColumn(
                name: "CatchLocationId",
                table: "SuperKatten");

            migrationBuilder.AddColumn<string>(
                name: "CatchLocation",
                table: "SuperKatten",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
