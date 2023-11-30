using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Domain.Data.Migrations
{
    public partial class PictureUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                schema: "cinema",
                table: "Movies",
                type: "text",
                nullable: false,
                defaultValue: string.Empty);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureUrl",
                schema: "cinema",
                table: "Movies");
        }
    }
}
