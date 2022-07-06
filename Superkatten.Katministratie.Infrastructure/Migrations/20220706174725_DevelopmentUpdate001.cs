using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Superkatten.Katministratie.Infrastructure.Migrations
{
    public partial class DevelopmentUpdate001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SuperKatten_Gastgezinnen_GastgezinDtoId",
                table: "SuperKatten");

            migrationBuilder.DropIndex(
                name: "IX_SuperKatten_GastgezinDtoId",
                table: "SuperKatten");

            migrationBuilder.RenameColumn(
                name: "GastgezinDtoId",
                table: "SuperKatten",
                newName: "GastgezinId");

            migrationBuilder.RenameColumn(
                name: "Area",
                table: "SuperKatten",
                newName: "LitterType");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SuperKatten",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CatArea",
                table: "SuperKatten",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "SuperKatten",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FoodType",
                table: "SuperKatten",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "WetFoodAllowed",
                table: "SuperKatten",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Gastgezinnen",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateTable(
                name: "MedicalProcedures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SuperkatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcedureType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalProcedures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Permissions = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalProcedures");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropColumn(
                name: "CatArea",
                table: "SuperKatten");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "SuperKatten");

            migrationBuilder.DropColumn(
                name: "FoodType",
                table: "SuperKatten");

            migrationBuilder.DropColumn(
                name: "WetFoodAllowed",
                table: "SuperKatten");

            migrationBuilder.RenameColumn(
                name: "LitterType",
                table: "SuperKatten",
                newName: "Area");

            migrationBuilder.RenameColumn(
                name: "GastgezinId",
                table: "SuperKatten",
                newName: "GastgezinDtoId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SuperKatten",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Gastgezinnen",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_SuperKatten_GastgezinDtoId",
                table: "SuperKatten",
                column: "GastgezinDtoId");

            migrationBuilder.AddForeignKey(
                name: "FK_SuperKatten_Gastgezinnen_GastgezinDtoId",
                table: "SuperKatten",
                column: "GastgezinDtoId",
                principalTable: "Gastgezinnen",
                principalColumn: "Id");
        }
    }
}
