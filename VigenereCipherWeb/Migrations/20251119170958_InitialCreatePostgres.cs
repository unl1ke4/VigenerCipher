using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VigenereCipherWeb.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreatePostgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Auth0UserId = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CipherMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CipherMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CipherJobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InputText = table.Column<string>(type: "text", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Operation = table.Column<string>(type: "text", nullable: false),
                    ResultText = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AppUserId = table.Column<int>(type: "integer", nullable: true),
                    CipherMethodId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CipherJobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CipherJobs_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CipherJobs_CipherMethods_CipherMethodId",
                        column: x => x.CipherMethodId,
                        principalTable: "CipherMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "Auth0UserId", "Email", "Username" },
                values: new object[,]
                {
                    { 1, "auth0|demo1", "demo@example.com", "demo_user" },
                    { 2, "auth0|demo2", "test@example.com", "test_user" }
                });

            migrationBuilder.InsertData(
                table: "CipherMethods",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Класичний шифр Віженера з буквеним ключем", "Vigenere" },
                    { 2, "Шифр Цезаря зі зсувом алфавіту", "Caesar" },
                    { 3, "Біграмний шифр Плейфера", "Playfair" }
                });

            migrationBuilder.InsertData(
                table: "CipherJobs",
                columns: new[] { "Id", "AppUserId", "CipherMethodId", "CreatedAt", "InputText", "Key", "Operation", "ResultText" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2025, 11, 15, 10, 30, 0, 0, DateTimeKind.Utc), "HELLO", "KEY", "encrypt", "RIJVS" },
                    { 2, 1, 1, new DateTime(2025, 11, 16, 14, 20, 0, 0, DateTimeKind.Utc), "WORLD", "KEY", "encrypt", "CPSME" },
                    { 3, 2, 2, new DateTime(2025, 11, 17, 9, 15, 0, 0, DateTimeKind.Utc), "SECRET", "ABC", "decrypt", "TFDSFU" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CipherJobs_AppUserId",
                table: "CipherJobs",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CipherJobs_CipherMethodId",
                table: "CipherJobs",
                column: "CipherMethodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CipherJobs");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "CipherMethods");
        }
    }
}
