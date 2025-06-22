using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hoodie.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderItemModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Orders__productI__4BAC3F29",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "productID",
                table: "Orders",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_productID",
                table: "Orders",
                newName: "IX_Orders_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Product_ProductId",
                table: "Orders",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "productID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Product_ProductId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Orders",
                newName: "productID");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_ProductId",
                table: "Orders",
                newName: "IX_Orders_productID");

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK__Orders__productI__4BAC3F29",
                table: "Orders",
                column: "productID",
                principalTable: "Product",
                principalColumn: "productID");
        }
    }
}
