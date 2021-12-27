using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwiftResume.DAL.EFCORE.Migrations
{
    public partial class EducacionExperienciaModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Responsabilidades",
                table: "Experiencia",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Lugar",
                table: "Experiencia",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Institucion",
                table: "Experiencia",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Experiencia",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResumeId",
                table: "Experiencia",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Institucion",
                table: "Educacion",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Educacion",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResumeId",
                table: "Educacion",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2021, 12, 26, 22, 1, 7, 850, DateTimeKind.Local).AddTicks(2406));

            migrationBuilder.CreateIndex(
                name: "IX_Experiencia_ResumeId",
                table: "Experiencia",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_Educacion_ResumeId",
                table: "Educacion",
                column: "ResumeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Educacion_Resumes_ResumeId",
                table: "Educacion",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Experiencia_Resumes_ResumeId",
                table: "Experiencia",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educacion_Resumes_ResumeId",
                table: "Educacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Experiencia_Resumes_ResumeId",
                table: "Experiencia");

            migrationBuilder.DropIndex(
                name: "IX_Experiencia_ResumeId",
                table: "Experiencia");

            migrationBuilder.DropIndex(
                name: "IX_Educacion_ResumeId",
                table: "Educacion");

            migrationBuilder.DropColumn(
                name: "ResumeId",
                table: "Experiencia");

            migrationBuilder.DropColumn(
                name: "ResumeId",
                table: "Educacion");

            migrationBuilder.AlterColumn<string>(
                name: "Responsabilidades",
                table: "Experiencia",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Lugar",
                table: "Experiencia",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Institucion",
                table: "Experiencia",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Experiencia",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Institucion",
                table: "Educacion",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Educacion",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2021, 12, 26, 21, 56, 48, 961, DateTimeKind.Local).AddTicks(9514));
        }
    }
}
