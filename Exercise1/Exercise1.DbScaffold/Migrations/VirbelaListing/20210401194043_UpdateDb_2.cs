using Microsoft.EntityFrameworkCore.Migrations;

namespace Exercise1.DbScaffold.Migrations.VirbelaListing
{
    public partial class UpdateDb_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "listinguser",
                columns: new[] { "id", "Email", "firstname", "lastname", "password", "region_id", "userid" },
                values: new object[] { 1, "jsmith@contoso.com", "John", "Smith", "AQAAAAEAACcQAAAAEDxkjJUF+sOyIinGOU2cO0YfvDtEP0hbK526b/qF/USWtT1fVwDVWf6yBKhAir9fCQ==", 1, "jsmith" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "listinguser",
                keyColumn: "id",
                keyValue: 1);
        }
    }
}
