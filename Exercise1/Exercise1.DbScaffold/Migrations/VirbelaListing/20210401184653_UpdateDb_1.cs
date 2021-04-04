using Microsoft.EntityFrameworkCore.Migrations;

namespace Exercise1.DbScaffold.Migrations.VirbelaListing
{
    public partial class UpdateDb_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "listinguser",
                unicode: false,
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "region",
                columns: new[] { "id", "name" },
                values: new object[] { 1, "A" });

            migrationBuilder.InsertData(
                table: "region",
                columns: new[] { "id", "name" },
                values: new object[] { 2, "B" });

            migrationBuilder.InsertData(
                table: "region",
                columns: new[] { "id", "name" },
                values: new object[] { 3, "C" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "region",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "region",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "region",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Email",
                table: "listinguser");
        }
    }
}
