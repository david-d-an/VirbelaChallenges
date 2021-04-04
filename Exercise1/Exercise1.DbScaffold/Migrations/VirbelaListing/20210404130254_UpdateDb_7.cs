using Microsoft.EntityFrameworkCore.Migrations;

namespace Exercise1.DbScaffold.Migrations.VirbelaListing
{
    public partial class UpdateDb_7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "listing",
                keyColumn: "id",
                keyValue: 1,
                column: "description",
                value: "Description for Listing A. Details for Listing A is provided here.");

            migrationBuilder.UpdateData(
                table: "listing",
                keyColumn: "id",
                keyValue: 2,
                column: "description",
                value: "Description for Listing B. Details for Listing B is provided here.");

            migrationBuilder.UpdateData(
                table: "listing",
                keyColumn: "id",
                keyValue: 3,
                column: "description",
                value: "Description for Listing C. Details for Listing C is provided here.");

            migrationBuilder.UpdateData(
                table: "listing",
                keyColumn: "id",
                keyValue: 4,
                column: "description",
                value: "Description for Listing D. Details for Listing D is provided here.");

            migrationBuilder.UpdateData(
                table: "listing",
                keyColumn: "id",
                keyValue: 5,
                column: "description",
                value: "Description for Listing E. Details for Listing E is provided here.");

            migrationBuilder.InsertData(
                table: "listinguser",
                columns: new[] { "id", "Email", "firstname", "lastname", "password", "region_id", "userid" },
                values: new object[] { 4, "maradona@contoso.com", "Diego", "Maradona", "AQAAAAEAACcQAAAAEDxkjJUF+sOyIinGOU2cO0YfvDtEP0hbK526b/qF/USWtT1fVwDVWf6yBKhAir9fCQ==", 1, "maradona" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "listinguser",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "listing",
                keyColumn: "id",
                keyValue: 1,
                column: "description",
                value: "");

            migrationBuilder.UpdateData(
                table: "listing",
                keyColumn: "id",
                keyValue: 2,
                column: "description",
                value: "");

            migrationBuilder.UpdateData(
                table: "listing",
                keyColumn: "id",
                keyValue: 3,
                column: "description",
                value: "");

            migrationBuilder.UpdateData(
                table: "listing",
                keyColumn: "id",
                keyValue: 4,
                column: "description",
                value: "");

            migrationBuilder.UpdateData(
                table: "listing",
                keyColumn: "id",
                keyValue: 5,
                column: "description",
                value: "");
        }
    }
}
