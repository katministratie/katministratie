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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    CatchDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CatchLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reserved = table.Column<bool>(type: "bit", nullable: false),
                    Retour = table.Column<bool>(type: "bit", nullable: false),
                    Area = table.Column<int>(type: "int", nullable: false),
                    CageNumber = table.Column<int>(type: "int", nullable: true),
                    Behaviour = table.Column<int>(type: "int", nullable: false),
                    GastgezinDtoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
