using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GeoSearch.DataAccessLayer.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("1fe4e147-de43-44f5-85ad-d1d24a0cfbb2"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("a9f585ae-33ae-4e31-82b1-5cf8719ab2f4"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("b4ceed95-f1c2-4ae2-a8f3-d7942c9364fd"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "ApiKey", "Username" },
                values: new object[,]
                {
                    { new Guid("1fe4e147-de43-44f5-85ad-d1d24a0cfbb2"), "apikey123", "member0" },
                    { new Guid("a9f585ae-33ae-4e31-82b1-5cf8719ab2f4"), "apikey456", "member1" },
                    { new Guid("b4ceed95-f1c2-4ae2-a8f3-d7942c9364fd"), "apikey789", "member2" }
                });
        }
    }
}
