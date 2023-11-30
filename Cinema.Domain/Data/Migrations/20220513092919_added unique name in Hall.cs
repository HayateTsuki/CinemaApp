using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema.Domain.Data.Migrations
{
    public partial class AddeduniquenameinHall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Halls_Name",
                schema: "cinema",
                table: "Halls",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Halls_Name",
                schema: "cinema",
                table: "Halls");
        }
    }
}
