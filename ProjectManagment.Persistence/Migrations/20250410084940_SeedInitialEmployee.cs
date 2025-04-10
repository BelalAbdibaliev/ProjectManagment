using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagment.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FirstName", "LastName", "MiddleName", "Email" },
                values: new object[] { 1, "Belal", "Abdibaliev", "Mederbekovish", "flacko@example.com" }
            );
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FirstName", "LastName", "MiddleName", "Email" },
                values: new object[] { 2, "Mirbek", "Polov", "Marat", "mirbek@example.com" }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1
            );
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2
            );
        }
    }
}
