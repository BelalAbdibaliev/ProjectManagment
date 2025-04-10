using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagment.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialCompanies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ClientCompanies",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Apple" });
            
            migrationBuilder.InsertData(
                table: "ClientCompanies",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Microsoft" });
            
            migrationBuilder.InsertData(
                table: "SupplierCompanies",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Meta" });
            
            migrationBuilder.InsertData(
                table: "SupplierCompanies",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "NASA" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClientCompanies",
                keyColumn: "Id",
                keyValue: 1);
            
            migrationBuilder.DeleteData(
                table: "ClientCompanies",
                keyColumn: "Id",
                keyValue: 2);
            
            migrationBuilder.DeleteData(
                table: "SupplierCompanies",
                keyColumn: "Id",
                keyValue: 1);
            
            migrationBuilder.DeleteData(
                table: "SupplierCompanies",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
