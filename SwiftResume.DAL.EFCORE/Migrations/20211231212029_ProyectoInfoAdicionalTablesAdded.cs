using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwiftResume.DAL.EFCORE.Migrations
{
    public partial class ProyectoInfoAdicionalTablesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InfoAdicional",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TipoInfoAdicional = table.Column<string>(type: "TEXT", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    ResumeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoAdicional", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfoAdicional_Resumes_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proyectos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TipoProyecto = table.Column<string>(type: "TEXT", nullable: false),
                    Titulo = table.Column<string>(type: "TEXT", nullable: false),
                    Link = table.Column<string>(type: "TEXT", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    ResumeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyectos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proyectos_Resumes_ResumeId",
                        column: x => x.ResumeId,
                        principalTable: "Resumes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2021, 12, 31, 16, 20, 28, 885, DateTimeKind.Local).AddTicks(3928));

            migrationBuilder.CreateIndex(
                name: "IX_InfoAdicional_ResumeId",
                table: "InfoAdicional",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_Proyectos_ResumeId",
                table: "Proyectos",
                column: "ResumeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InfoAdicional");

            migrationBuilder.DropTable(
                name: "Proyectos");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateJoined",
                value: new DateTime(2021, 12, 29, 23, 33, 5, 437, DateTimeKind.Local).AddTicks(3134));
        }
    }
}
