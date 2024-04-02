using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Awushi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class imagename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "products");
        }
    }
}
