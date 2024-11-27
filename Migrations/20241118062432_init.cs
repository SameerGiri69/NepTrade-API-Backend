using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Finshark_API.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53c56eb7-80bf-4cfb-9917-e58a3b8e1616");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c4c49a0f-620c-4452-89bc-339b45260781");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "59d7e508-b109-4314-a82f-7a5c2bbb91f7", null, "Admin", "ADMIN" },
                    { "d94fab4b-14ed-42b9-ae3a-d4e06315917b", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "59d7e508-b109-4314-a82f-7a5c2bbb91f7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d94fab4b-14ed-42b9-ae3a-d4e06315917b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "53c56eb7-80bf-4cfb-9917-e58a3b8e1616", null, "User", "USER" },
                    { "c4c49a0f-620c-4452-89bc-339b45260781", null, "Admin", "ADMIN" }
                });
        }
    }
}
