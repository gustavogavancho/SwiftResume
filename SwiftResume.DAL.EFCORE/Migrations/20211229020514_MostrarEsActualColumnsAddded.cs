using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwiftResume.DAL.EFCORE.Migrations
{
    public partial class MostrarEsActualColumnsAddded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EsActual",
                table: "Experiencia",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Mostrar",
                table: "Experiencia",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TipoEducacion",
                table: "Educacion",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2021, 12, 28, 21, 5, 13, 850, DateTimeKind.Local).AddTicks(9216));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EsActual",
                table: "Experiencia");

            migrationBuilder.DropColumn(
                name: "Mostrar",
                table: "Experiencia");

            migrationBuilder.DropColumn(
                name: "TipoEducacion",
                table: "Educacion");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2021, 12, 27, 12, 48, 48, 244, DateTimeKind.Local).AddTicks(8928));
        }
    }
}
