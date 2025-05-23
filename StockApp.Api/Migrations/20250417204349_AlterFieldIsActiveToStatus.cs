using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class AlterFieldIsActiveToStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Product",
                newName: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Product",
                newName: "IsActive");
        }
    }
}
