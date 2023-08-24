using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookVerse.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCartDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_AspNetUsers_ApplicationUser",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ApplicationUser",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ApplicationUser",
                table: "Carts");

            migrationBuilder.AddColumn<double>(
                name: "CartPrice",
                table: "Carts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "BasketItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "NetPrice",
                table: "BasketItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_CartId",
                table: "BasketItems",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Carts_CartId",
                table: "BasketItems",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Carts_CartId",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_CartId",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "CartPrice",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "NetPrice",
                table: "BasketItems");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUser",
                table: "Carts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ApplicationUser",
                table: "Carts",
                column: "ApplicationUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_AspNetUsers_ApplicationUser",
                table: "Carts",
                column: "ApplicationUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
