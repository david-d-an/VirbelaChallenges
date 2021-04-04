using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Exercise1.DbScaffold.Migrations.VirbelaListing
{
    public partial class UpdateDb_8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "listing",
                columns: new[] { "id", "created_date", "creator_id", "description", "price", "title" },
                values: new object[] { 6, new DateTime(2021, 4, 1, 19, 43, 58, 0, DateTimeKind.Unspecified), 4, "Description for Listing F. Details for Listing F is provided here.", 489.99m, "Listing F" });

            migrationBuilder.InsertData(
                table: "listing",
                columns: new[] { "id", "created_date", "creator_id", "description", "price", "title" },
                values: new object[] { 7, new DateTime(2021, 3, 31, 8, 22, 38, 0, DateTimeKind.Unspecified), 4, "Description for Listing G. Details for Listing G is provided here.", 124.99m, "Listing G" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "listing",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "listing",
                keyColumn: "id",
                keyValue: 7);
        }
    }
}
