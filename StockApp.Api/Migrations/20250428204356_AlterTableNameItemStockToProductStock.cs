using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableNameItemStockToProductStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemStock_Location_LocationId",
                table: "ItemStock");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemStock_Product_ItemId",
                table: "ItemStock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemStock",
                table: "ItemStock");

            migrationBuilder.RenameTable(
                name: "ItemStock",
                newName: "ProductStock");

            migrationBuilder.RenameIndex(
                name: "IX_ItemStock_LocationId_ItemId",
                table: "ProductStock",
                newName: "IX_ProductStock_LocationId_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemStock_ItemId",
                table: "ProductStock",
                newName: "IX_ProductStock_ItemId");

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "ProductStock",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductStock",
                table: "ProductStock",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStock_ProductId",
                table: "ProductStock",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStock_Location_LocationId",
                table: "ProductStock",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStock_Product_ItemId",
                table: "ProductStock",
                column: "ItemId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStock_Product_ProductId",
                table: "ProductStock",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductStock_Location_LocationId",
                table: "ProductStock");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductStock_Product_ItemId",
                table: "ProductStock");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductStock_Product_ProductId",
                table: "ProductStock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductStock",
                table: "ProductStock");

            migrationBuilder.DropIndex(
                name: "IX_ProductStock_ProductId",
                table: "ProductStock");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductStock");

            migrationBuilder.RenameTable(
                name: "ProductStock",
                newName: "ItemStock");

            migrationBuilder.RenameIndex(
                name: "IX_ProductStock_LocationId_ItemId",
                table: "ItemStock",
                newName: "IX_ItemStock_LocationId_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductStock_ItemId",
                table: "ItemStock",
                newName: "IX_ItemStock_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemStock",
                table: "ItemStock",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemStock_Location_LocationId",
                table: "ItemStock",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemStock_Product_ItemId",
                table: "ItemStock",
                column: "ItemId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
