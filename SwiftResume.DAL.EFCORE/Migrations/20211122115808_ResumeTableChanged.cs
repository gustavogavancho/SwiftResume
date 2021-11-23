using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwiftResume.DAL.EFCORE.Migrations
{
    public partial class ResumeTableChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Perfil_PerfilId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PerfilId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PerfilId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "PerfilId",
                table: "Resumes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2021, 11, 22, 6, 58, 7, 907, DateTimeKind.Local).AddTicks(8728));

            migrationBuilder.CreateIndex(
                name: "IX_Resumes_PerfilId",
                table: "Resumes",
                column: "PerfilId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resumes_Perfil_PerfilId",
                table: "Resumes",
                column: "PerfilId",
                principalTable: "Perfil",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resumes_Perfil_PerfilId",
                table: "Resumes");

            migrationBuilder.DropIndex(
                name: "IX_Resumes_PerfilId",
                table: "Resumes");

            migrationBuilder.DropColumn(
                name: "PerfilId",
                table: "Resumes");

            migrationBuilder.AddColumn<int>(
                name: "PerfilId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

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
    }
}
