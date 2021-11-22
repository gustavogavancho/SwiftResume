using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwiftResume.DAL.EFCORE.Migrations
{
    public partial class PerfilTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PerfilId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Perfil",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Profesion = table.Column<string>(type: "TEXT", nullable: false),
                    Resumen = table.Column<string>(type: "TEXT", nullable: false),
                    Direccion = table.Column<string>(type: "TEXT", nullable: false),
                    Telefono = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Email2 = table.Column<string>(type: "TEXT", nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Linkedin = table.Column<string>(type: "TEXT", nullable: true),
                    Github = table.Column<string>(type: "TEXT", nullable: true),
                    Blog = table.Column<string>(type: "TEXT", nullable: true),
                    StackOverflow = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfil", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2021, 11, 20, 21, 8, 33, 686, DateTimeKind.Local).AddTicks(2589));

            migrationBuilder.CreateIndex(
                name: "IX_Users_PerfilId",
                table: "Users",
                column: "PerfilId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Perfil_PerfilId",
                table: "Users",
                column: "PerfilId",
                principalTable: "Perfil",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Perfil_PerfilId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Perfil");

            migrationBuilder.DropIndex(
                name: "IX_Users_PerfilId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PerfilId",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2021, 11, 20, 17, 39, 33, 96, DateTimeKind.Local).AddTicks(7570));
        }
    }
}
