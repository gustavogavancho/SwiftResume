using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwiftResume.DAL.EFCORE.Migrations
{
    public partial class ChangesPart1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Perfil_Resumes_ResumeId",
                table: "Perfil");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Perfil",
                table: "Perfil");

            migrationBuilder.RenameTable(
                name: "Perfil",
                newName: "Perfiles");

            migrationBuilder.RenameIndex(
                name: "IX_Perfil_ResumeId",
                table: "Perfiles",
                newName: "IX_Perfiles_ResumeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Perfiles",
                table: "Perfiles",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2021, 11, 29, 22, 14, 15, 896, DateTimeKind.Local).AddTicks(437));

            migrationBuilder.AddForeignKey(
                name: "FK_Perfiles_Resumes_ResumeId",
                table: "Perfiles",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Perfiles_Resumes_ResumeId",
                table: "Perfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Perfiles",
                table: "Perfiles");

            migrationBuilder.RenameTable(
                name: "Perfiles",
                newName: "Perfil");

            migrationBuilder.RenameIndex(
                name: "IX_Perfiles_ResumeId",
                table: "Perfil",
                newName: "IX_Perfil_ResumeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Perfil",
                table: "Perfil",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2021, 11, 23, 16, 29, 20, 195, DateTimeKind.Local).AddTicks(2160));

            migrationBuilder.AddForeignKey(
                name: "FK_Perfil_Resumes_ResumeId",
                table: "Perfil",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
