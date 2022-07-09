using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Superkatten.Katministratie.Infrastructure.Migrations
{
    public partial class Update_AddSuperkatState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsKitten",
                table: "SuperKatten");

            migrationBuilder.AddColumn<int>(
                name: "AgeCategory",
                table: "SuperKatten",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "SuperKatten",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgeCategory",
                table: "SuperKatten");

            migrationBuilder.DropColumn(
                name: "State",
                table: "SuperKatten");

            migrationBuilder.AddColumn<bool>(
                name: "IsKitten",
                table: "SuperKatten",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
