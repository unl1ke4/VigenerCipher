using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VigenereCipherWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddCipherMethodsAndUpdateSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CipherMethodId",
                table: "CipherJobs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CipherJobs",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "CipherMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CipherMethods", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "Auth0UserId", "Email", "Username" },
                values: new object[,]
                {
                    { 1, "auth0|demo1", "demo@example.com", "demo_user" },
                    { 2, "auth0|demo2", "test@example.com", "test_user" }
                });

            migrationBuilder.UpdateData(
                table: "CipherJobs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AppUserId", "CipherMethodId", "CreatedAt" },
                values: new object[] { 1, 1, new DateTime(2025, 11, 15, 10, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "CipherJobs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AppUserId", "CipherMethodId", "CreatedAt" },
                values: new object[] { 1, 1, new DateTime(2025, 11, 16, 14, 20, 0, 0, DateTimeKind.Unspecified) });

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
                values: new object[] { 3, 2, 2, new DateTime(2025, 11, 17, 9, 15, 0, 0, DateTimeKind.Unspecified), "SECRET", "ABC", "decrypt", "TFDSFU" });

            migrationBuilder.CreateIndex(
                name: "IX_CipherJobs_CipherMethodId",
                table: "CipherJobs",
                column: "CipherMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_CipherJobs_CipherMethods_CipherMethodId",
                table: "CipherJobs",
                column: "CipherMethodId",
                principalTable: "CipherMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CipherJobs_CipherMethods_CipherMethodId",
                table: "CipherJobs");

            migrationBuilder.DropTable(
                name: "CipherMethods");

            migrationBuilder.DropIndex(
                name: "IX_CipherJobs_CipherMethodId",
                table: "CipherJobs");

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CipherJobs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "CipherMethodId",
                table: "CipherJobs");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CipherJobs");

            migrationBuilder.UpdateData(
                table: "CipherJobs",
                keyColumn: "Id",
                keyValue: 1,
                column: "AppUserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "CipherJobs",
                keyColumn: "Id",
                keyValue: 2,
                column: "AppUserId",
                value: null);
        }
    }
}
