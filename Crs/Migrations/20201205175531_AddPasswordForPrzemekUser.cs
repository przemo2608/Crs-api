using Microsoft.EntityFrameworkCore.Migrations;

namespace Crs.Migrations
{
    public partial class AddPasswordForPrzemekUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAEAACcQAAAAEEnhJf4q8uFsppZzvag89dslA8SFIaDC5l3SaEPtUse6CKJ7pcqQLngZgeR/oOENrA==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "");
        }
    }
}
