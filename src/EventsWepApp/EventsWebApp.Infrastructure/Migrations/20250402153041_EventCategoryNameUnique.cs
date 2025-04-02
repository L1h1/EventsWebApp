using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsWebApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EventCategoryNameUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EventCategories_Name",
                table: "EventCategories",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EventCategories_Name",
                table: "EventCategories");
        }
    }
}
