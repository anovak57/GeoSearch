using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoSearch.DataAccessLayer.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGeoLocationModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Query",
                table: "GeoLocationSearches",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Radius",
                table: "GeoLocationSearches",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "GeoLocations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<List<string>>(
                name: "Categories",
                table: "GeoLocations",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "GeoLocations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "GeoLocations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "GeoLocations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "GeoLocations",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Query",
                table: "GeoLocationSearches");

            migrationBuilder.DropColumn(
                name: "Radius",
                table: "GeoLocationSearches");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "GeoLocations");

            migrationBuilder.DropColumn(
                name: "Categories",
                table: "GeoLocations");

            migrationBuilder.DropColumn(
                name: "City",
                table: "GeoLocations");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "GeoLocations");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "GeoLocations");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "GeoLocations");
        }
    }
}
