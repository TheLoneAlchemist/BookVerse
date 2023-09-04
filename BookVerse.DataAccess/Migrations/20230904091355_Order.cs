using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookVerse.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Carts_CartId",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "AddedOn",
                table: "BasketItems");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "BasketItems",
                newName: "OrderStatus");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Carts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CheckOutStatus",
                table: "Carts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "GrandTotalPrice",
                table: "Carts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ItemCount",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "BasketItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Checkouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    ShippingPrice = table.Column<int>(type: "int", nullable: false),
                    CouponDiscount = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Checkouts_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Checkouts_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckoutId = table.Column<int>(type: "int", nullable: false),
                    PayementMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderStatus = table.Column<bool>(type: "bit", nullable: false),
                    PaymentStatus = table.Column<bool>(type: "bit", nullable: false),
                    DeliveryStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Checkouts_CheckoutId",
                        column: x => x.CheckoutId,
                        principalTable: "Checkouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderedItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    NetPrice = table.Column<double>(type: "float", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderedItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderedItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderedItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ApplicationUserId",
                table: "Carts",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkouts_AddressId",
                table: "Checkouts",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkouts_CartId",
                table: "Checkouts",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedItems_OrderId",
                table: "OrderedItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderedItems_ProductId",
                table: "OrderedItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CheckoutId",
                table: "Orders",
                column: "CheckoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Carts_CartId",
                table: "BasketItems",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_AspNetUsers_ApplicationUserId",
                table: "Carts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Carts_CartId",
                table: "BasketItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_AspNetUsers_ApplicationUserId",
                table: "Carts");

            migrationBuilder.DropTable(
                name: "OrderedItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Checkouts");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ApplicationUserId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "CheckOutStatus",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "GrandTotalPrice",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ItemCount",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "OrderStatus",
                table: "BasketItems",
                newName: "Status");

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "BasketItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedOn",
                table: "BasketItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Carts_CartId",
                table: "BasketItems",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id");
        }
    }
}
