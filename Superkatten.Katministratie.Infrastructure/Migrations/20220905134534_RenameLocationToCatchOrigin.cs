using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Superkatten.Katministratie.Infrastructure.Migrations
{
    public partial class RenameLocationToCatchOrigin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SuperKatten_Locations_CatchLocationId",
                table: "SuperKatten");

            migrationBuilder.RenameColumn(
                name: "CatchLocationId",
                table: "SuperKatten",
                newName: "CatchOriginId");

            migrationBuilder.RenameIndex(
                name: "IX_SuperKatten_CatchLocationId",
                table: "SuperKatten",
                newName: "IX_SuperKatten_CatchOriginId");

            migrationBuilder.CreateTable(
                name: "CatchOrigins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatchOrigins", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_SuperKatten_CatchOrigins_CatchOriginId",
                table: "SuperKatten",
                column: "CatchOriginId",
                principalTable: "CatchOrigins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.Sql("INSERT INTO CatchOrigin SELECT * FROM Locations");

            migrationBuilder.DropTable(
                name: "Locations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SuperKatten_CatchOrigins_CatchOriginId",
                table: "SuperKatten");

            migrationBuilder.DropTable(
                name: "CatchOrigins");

            migrationBuilder.RenameColumn(
                name: "CatchOriginId",
                table: "SuperKatten",
                newName: "CatchLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_SuperKatten_CatchOriginId",
                table: "SuperKatten",
                newName: "IX_SuperKatten_CatchLocationId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_SuperKatten_Locations_CatchLocationId",
                table: "SuperKatten",
                column: "CatchLocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
