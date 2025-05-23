using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class newTableMovement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movement",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    OriginLocationId = table.Column<long>(type: "bigint", nullable: true),
                    DestinationLocationId = table.Column<long>(type: "bigint", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MovementType = table.Column<short>(type: "SMALLINT", nullable: false),
                    MovementDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(500)", maxLength: 500, nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movement_Location_DestinationLocationId",
                        column: x => x.DestinationLocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movement_Location_OriginLocationId",
                        column: x => x.OriginLocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movement_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movement_DestinationLocationId",
                table: "Movement",
                column: "DestinationLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Movement_OriginLocationId",
                table: "Movement",
                column: "OriginLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Movement_ProductId",
                table: "Movement",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movement");
        }
    }
}
