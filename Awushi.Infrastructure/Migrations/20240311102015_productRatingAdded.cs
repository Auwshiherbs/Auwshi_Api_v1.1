using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Awushi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class productRatingAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "products");
        }
    }
}
