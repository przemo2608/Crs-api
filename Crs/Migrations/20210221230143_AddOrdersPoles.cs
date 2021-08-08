using Microsoft.EntityFrameworkCore.Migrations;

namespace Crs.Migrations
{
    public partial class AddOrdersPoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CarBrand",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarModel",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarBrand",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CarModel",
                table: "Orders");
        }
    }
}
