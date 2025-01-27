using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebsiteASPNETCOREMVC.Migrations.BlogDB
{
    /// <inheritdoc />
    public partial class WebFirstV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FeaturedImage",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeaturedImage",
                table: "Blogs");
        }
    }
}
