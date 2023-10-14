using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LoginApplication.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolesTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8b0ad113-01f7-4166-8af2-95d50a2ace1d", "e68fa575-8f87-4924-ac76-d1cb478b9840", "User", "USER" },
                    { "c828f6a6-be99-4e1a-8ede-93fed677aa8a", "5b88549e-0d66-4c46-9eda-6c9657e8c7e5", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b0ad113-01f7-4166-8af2-95d50a2ace1d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c828f6a6-be99-4e1a-8ede-93fed677aa8a");
        }
    }
}
