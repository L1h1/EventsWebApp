using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsWebApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ComputableImageForEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "Events",
                type: "TEXT",
                nullable: false,
                computedColumnSql: "'photos/events/' || CAST(id AS TEXT)",
                oldClrType: typeof(string),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "Events",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldComputedColumnSql: "'photos/events/' || CAST(id AS TEXT)");
        }
    }
}
