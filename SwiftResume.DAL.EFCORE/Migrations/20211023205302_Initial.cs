using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SwiftResume.DAL.EFCORE.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Resumes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombres = table.Column<string>(type: "TEXT", nullable: true),
                    Apellidos = table.Column<string>(type: "TEXT", nullable: true),
                    Genero = table.Column<string>(type: "TEXT", nullable: true),
                    Lenguaje = table.Column<string>(type: "TEXT", nullable: true),
                    Foto = table.Column<byte[]>(type: "BLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resumes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Resumes",
                columns: new[] { "Id", "Apellidos", "Foto", "Genero", "Lenguaje", "Nombres" },
                values: new object[] { 1, "Gavancho León", null, "Masculino", "Español", "Gustavo" });

            migrationBuilder.InsertData(
                table: "Resumes",
                columns: new[] { "Id", "Apellidos", "Foto", "Genero", "Lenguaje", "Nombres" },
                values: new object[] { 2, "Gavancho León", null, "Masculino", "Español", "Jordi" });

            migrationBuilder.InsertData(
                table: "Resumes",
                columns: new[] { "Id", "Apellidos", "Foto", "Genero", "Lenguaje", "Nombres" },
                values: new object[] { 3, "Iñipe Cachay", null, "Femenino", "English", "Milagros" });

            migrationBuilder.InsertData(
                table: "Resumes",
                columns: new[] { "Id", "Apellidos", "Foto", "Genero", "Lenguaje", "Nombres" },
                values: new object[] { 4, "Gavancho León", null, "Femenino", "English", "Olga Cristina del Rocio" });

            migrationBuilder.InsertData(
                table: "Resumes",
                columns: new[] { "Id", "Apellidos", "Foto", "Genero", "Lenguaje", "Nombres" },
                values: new object[] { 5, "León García", null, "Femenino", "English", "Olga del Rocio" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resumes");
        }
    }
}
