using Microsoft.EntityFrameworkCore.Migrations;

namespace SwiftResume.DAL.EFCORE.Migrations
{
    public partial class ModifyLanguejeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Language",
                table: "Resumes",
                newName: "Lenguaje");

            migrationBuilder.InsertData(
                table: "Resumes",
                columns: new[] { "Id", "Apellidos", "Foto", "Genero", "Lenguaje", "Nombres" },
                values: new object[] { 1, "Gavancho León", null, "Masculino", "Español", "Gustavo" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Resumes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "Lenguaje",
                table: "Resumes",
                newName: "Language");
        }
    }
}
