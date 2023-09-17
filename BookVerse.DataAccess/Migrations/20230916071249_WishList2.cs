using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookVerse.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class WishList2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "WishLists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WishLists");
        }
    }
}
