using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace smERP.Migrations
{
    /// <inheritdoc />
    public partial class AddingThumbnailAndFullscreenToCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LogoImage",
                table: "Company",
                newName: "ThumbnailLogoImage");

            migrationBuilder.RenameColumn(
                name: "CoverImage",
                table: "Company",
                newName: "ThumbnailCoverImage");

            migrationBuilder.AddColumn<string>(
                name: "FullscreenCoverImage",
                table: "Company",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullscreenLogoImage",
                table: "Company",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullscreenCoverImage",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "FullscreenLogoImage",
                table: "Company");

            migrationBuilder.RenameColumn(
                name: "ThumbnailLogoImage",
                table: "Company",
                newName: "LogoImage");

            migrationBuilder.RenameColumn(
                name: "ThumbnailCoverImage",
                table: "Company",
                newName: "CoverImage");
        }
    }
}
