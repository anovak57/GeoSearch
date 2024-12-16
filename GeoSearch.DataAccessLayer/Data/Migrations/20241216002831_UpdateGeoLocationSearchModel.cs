using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoSearch.DataAccessLayer.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGeoLocationSearchModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeoLocationSearches_Users_UserId",
                table: "GeoLocationSearches");

            migrationBuilder.DropIndex(
                name: "IX_GeoLocationSearches_UserId",
                table: "GeoLocationSearches");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GeoLocationSearches");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "GeoLocationSearches",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_GeoLocationSearches_UserId",
                table: "GeoLocationSearches",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GeoLocationSearches_Users_UserId",
                table: "GeoLocationSearches",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
