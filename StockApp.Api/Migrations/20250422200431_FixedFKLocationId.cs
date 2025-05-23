using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixedFKLocationId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemStock_Location_ItemId",
                table: "ItemStock");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemStock_Location_LocationId",
                table: "ItemStock",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemStock_Location_LocationId",
                table: "ItemStock");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemStock_Location_ItemId",
                table: "ItemStock",
                column: "ItemId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
