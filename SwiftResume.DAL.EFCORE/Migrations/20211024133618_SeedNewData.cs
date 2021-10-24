using Microsoft.EntityFrameworkCore.Migrations;

namespace SwiftResume.DAL.EFCORE.Migrations
{
    public partial class SeedNewData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Resumes",
                columns: new[] { "Id", "Apellidos", "Foto", "Genero", "Lenguaje", "Nombres" },
                values: new object[] { 6, "Cordiva Rios", null, "Femenino", "English", "Toty Ernestina" });

            migrationBuilder.InsertData(
                table: "Resumes",
                columns: new[] { "Id", "Apellidos", "Foto", "Genero", "Lenguaje", "Nombres" },
                values: new object[] { 7, "Gavancho Cordova", null, "Femenino", "English", "Mia Isabella" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Resumes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Resumes",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
