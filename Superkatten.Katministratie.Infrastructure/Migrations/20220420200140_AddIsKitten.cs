using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Superkatten.Katministratie.Infrastructure.Migrations
{
    public partial class AddIsKitten : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsKitten",
                table: "SuperKatten",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsKitten",
                table: "SuperKatten");
        }
    }
}
