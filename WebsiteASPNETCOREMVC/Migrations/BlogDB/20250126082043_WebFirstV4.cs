using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebsiteASPNETCOREMVC.Migrations.BlogDB
{
    /// <inheritdoc />
    public partial class WebFirstV4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FeaturedImage",
                table: "Blogs",
                newName: "FilePath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Blogs",
                newName: "FeaturedImage");
        }
    }
}
