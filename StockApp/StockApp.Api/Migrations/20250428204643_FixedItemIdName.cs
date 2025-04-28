using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixedItemIdName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductStock_Product_ItemId",
                table: "ProductStock");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductStock_Product_ProductId",
                table: "ProductStock");

            migrationBuilder.DropIndex(
                name: "IX_ProductStock_LocationId_ItemId",
                table: "ProductStock");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "ProductStock");

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                table: "ProductStock",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProductId1",
                table: "ProductStock",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductStock_LocationId_ProductId",
                table: "ProductStock",
                columns: new[] { "LocationId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductStock_ProductId1",
                table: "ProductStock",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStock_Product_ProductId",
                table: "ProductStock",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStock_Product_ProductId1",
                table: "ProductStock",
                column: "ProductId1",
                principalTable: "Product",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductStock_Product_ProductId",
                table: "ProductStock");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductStock_Product_ProductId1",
                table: "ProductStock");

            migrationBuilder.DropIndex(
                name: "IX_ProductStock_LocationId_ProductId",
                table: "ProductStock");

            migrationBuilder.DropIndex(
                name: "IX_ProductStock_ProductId1",
                table: "ProductStock");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "ProductStock");

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                table: "ProductStock",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "ItemId",
                table: "ProductStock",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ProductStock_ItemId",
                table: "ProductStock",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStock_LocationId_ItemId",
                table: "ProductStock",
                columns: new[] { "LocationId", "ItemId" },
                unique: true);

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
    }
}
