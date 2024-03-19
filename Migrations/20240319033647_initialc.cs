using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Finshark_API.Migrations
{
    /// <inheritdoc />
    public partial class initialc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a4775bb3-e0e6-4123-9870-35cc7a0ed171");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ebde47a0-d378-40cb-8d23-02561103d6ed");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "53c56eb7-80bf-4cfb-9917-e58a3b8e1616", null, "User", "USER" },
                    { "c4c49a0f-620c-4452-89bc-339b45260781", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "a4775bb3-e0e6-4123-9870-35cc7a0ed171", null, "Admin", "ADMIN" },
                    { "ebde47a0-d378-40cb-8d23-02561103d6ed", null, "User", "USER" }
                });
        }
    }
}
