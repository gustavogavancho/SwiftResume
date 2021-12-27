using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwiftResume.DAL.EFCORE.Migrations
{
    public partial class EducacionExperienciaAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Educacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Institucion = table.Column<string>(type: "TEXT", nullable: true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: true),
                    Meritos = table.Column<string>(type: "TEXT", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Experiencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Institucion = table.Column<string>(type: "TEXT", nullable: true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: true),
                    Lugar = table.Column<string>(type: "TEXT", nullable: true),
                    Responsabilidades = table.Column<string>(type: "TEXT", nullable: true),
                    Logros = table.Column<string>(type: "TEXT", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiencia", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2021, 12, 26, 21, 56, 48, 961, DateTimeKind.Local).AddTicks(9514));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Educacion");

            migrationBuilder.DropTable(
                name: "Experiencia");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2021, 12, 20, 22, 49, 33, 214, DateTimeKind.Local).AddTicks(5002));
        }
    }
}
