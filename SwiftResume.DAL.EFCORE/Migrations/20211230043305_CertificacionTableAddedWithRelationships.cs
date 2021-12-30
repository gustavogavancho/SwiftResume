using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwiftResume.DAL.EFCORE.Migrations
{
    public partial class CertificacionTableAddedWithRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResumeId",
                table: "Certificaciones",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2021, 12, 29, 23, 33, 5, 437, DateTimeKind.Local).AddTicks(3134));

            migrationBuilder.CreateIndex(
                name: "IX_Certificaciones_ResumeId",
                table: "Certificaciones",
                column: "ResumeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificaciones_Resumes_ResumeId",
                table: "Certificaciones",
                column: "ResumeId",
                principalTable: "Resumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificaciones_Resumes_ResumeId",
                table: "Certificaciones");

            migrationBuilder.DropIndex(
                name: "IX_Certificaciones_ResumeId",
                table: "Certificaciones");

            migrationBuilder.DropColumn(
                name: "ResumeId",
                table: "Certificaciones");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2021, 12, 29, 23, 31, 26, 873, DateTimeKind.Local).AddTicks(4017));
        }
    }
}
