using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoSite.Migrations
{
    /// <inheritdoc />
    public partial class _3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posts_UserId",
                table: "Posts");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId_Title",
                table: "Posts",
                columns: new[] { "UserId", "Title" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posts_UserId_Title",
                table: "Posts");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");
        }
    }
}
