using Microsoft.EntityFrameworkCore.Migrations;

namespace Crs.Migrations
{
    public partial class dataatstart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Password", "RoleId", "Surname", "Username" },
                values: new object[] { "Klient", "AQAAAAEAACcQAAAAEI/95W3RVYvkKzfGU7R5FygehcQY+b2MuiEikNY9ozfOtNxJIWZzZEqClqVLHIhuXw==", 1, "Testowy", "Klient123" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "RoleId", "Surname", "Username" },
                values: new object[] { 3, null, "Admin", "AQAAAAEAACcQAAAAENJWyPXJfJkc5R/gBF4Q7zBhVpGXl5XrBBylEHWwr7eaCjM9sB7pA7k1NQ4g1aW9mw==", 3, "Admin", "Admin123" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "RoleId", "Surname", "Username" },
                values: new object[] { 2, null, "Mechanik", "AQAAAAEAACcQAAAAEOxDc8W6wJqTqknZ9mlMGrU2V8G8L+3kRXuHKLZiOCFVw52hGyyZME7OMKbl8nkadg==", 2, "Testowy", "Mechanik123" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Password", "RoleId", "Surname", "Username" },
                values: new object[] { null, "AQAAAAEAACcQAAAAEEnhJf4q8uFsppZzvag89dslA8SFIaDC5l3SaEPtUse6CKJ7pcqQLngZgeR/oOENrA==", 3, null, "Przemek" });
        }
    }
}
