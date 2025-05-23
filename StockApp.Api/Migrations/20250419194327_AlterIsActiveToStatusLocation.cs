using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class AlterIsActiveToStatusLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_CustomId",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Location",
                newName: "Status");

            migrationBuilder.AlterColumn<string>(
                name: "CustomId",
                table: "Product",
                type: "VARCHAR(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_CustomId",
                table: "Product",
                column: "CustomId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_CustomId",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Location",
                newName: "IsActive");

            migrationBuilder.AlterColumn<string>(
                name: "CustomId",
                table: "Product",
                type: "VARCHAR(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(20)",
                oldMaxLength: 20);

            migrationBuilder.CreateIndex(
                name: "IX_Product_CustomId",
                table: "Product",
                column: "CustomId",
                unique: true,
                filter: "[CustomId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
