using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Superkatten.Katministratie.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gastgezinnen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gastgezinnen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuperKatten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    FoundDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Birthday = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CatchLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuperkatColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsGoingRetour = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GastgezinDtoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperKatten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuperKatten_Gastgezinnen_GastgezinDtoId",
                        column: x => x.GastgezinDtoId,
                        principalTable: "Gastgezinnen",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SuperKatten_GastgezinDtoId",
                table: "SuperKatten",
                column: "GastgezinDtoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuperKatten");

            migrationBuilder.DropTable(
                name: "Gastgezinnen");
        }
    }
}
