using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixedDuplicateProductId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductStock_Product_ProductId1",
                table: "ProductStock");

            migrationBuilder.DropIndex(
                name: "IX_ProductStock_ProductId1",
                table: "ProductStock");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "ProductStock");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProductId1",
                table: "ProductStock",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductStock_ProductId1",
                table: "ProductStock",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStock_Product_ProductId1",
                table: "ProductStock",
                column: "ProductId1",
                principalTable: "Product",
                principalColumn: "Id");
        }
    }
}
