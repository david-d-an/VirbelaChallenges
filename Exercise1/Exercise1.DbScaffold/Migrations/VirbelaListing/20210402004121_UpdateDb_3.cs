using Microsoft.EntityFrameworkCore.Migrations;

namespace Exercise1.DbScaffold.Migrations.VirbelaListing
{
    public partial class UpdateDb_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "listinguser",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "region",
                columns: new[] { "id", "name" },
                values: new object[] { 4, "D" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "region",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.InsertData(
                table: "listinguser",
                columns: new[] { "id", "Email", "firstname", "lastname", "password", "region_id", "userid" },
                values: new object[] { 1, "jsmith@contoso.com", "John", "Smith", "AQAAAAEAACcQAAAAEDxkjJUF+sOyIinGOU2cO0YfvDtEP0hbK526b/qF/USWtT1fVwDVWf6yBKhAir9fCQ==", 1, "jsmith" });
        }
    }
}
