using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoSearch.DataAccessLayer.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRadiusDataType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Radius",
                table: "GeoLocationSearches",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Radius",
                table: "GeoLocationSearches",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
