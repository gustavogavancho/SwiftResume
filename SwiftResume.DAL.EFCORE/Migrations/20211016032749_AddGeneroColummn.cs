using Microsoft.EntityFrameworkCore.Migrations;

namespace SwiftResume.DAL.EFCORE.Migrations
{
    public partial class AddGeneroColummn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Genero",
                table: "Resumes",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genero",
                table: "Resumes");
        }
    }
}
