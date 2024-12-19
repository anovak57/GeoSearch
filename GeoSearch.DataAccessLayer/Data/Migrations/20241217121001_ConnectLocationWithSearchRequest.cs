using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoSearch.DataAccessLayer.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConnectLocationWithSearchRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeoLocationGeoLocationSearch",
                columns: table => new
                {
                    GeoLocationSearchesId = table.Column<int>(type: "integer", nullable: false),
                    GeoLocationsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoLocationGeoLocationSearch", x => new { x.GeoLocationSearchesId, x.GeoLocationsId });
                    table.ForeignKey(
                        name: "FK_GeoLocationGeoLocationSearch_GeoLocationSearches_GeoLocatio~",
                        column: x => x.GeoLocationSearchesId,
                        principalTable: "GeoLocationSearches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeoLocationGeoLocationSearch_GeoLocations_GeoLocationsId",
                        column: x => x.GeoLocationsId,
                        principalTable: "GeoLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeoLocationGeoLocationSearch_GeoLocationsId",
                table: "GeoLocationGeoLocationSearch",
                column: "GeoLocationsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeoLocationGeoLocationSearch");
        }
    }
}
