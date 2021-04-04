using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Exercise1.DbScaffold.Migrations.VirbelaListing
{
    public partial class UpdateDb_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "listing",
                columns: new[] { "id", "created_date", "creator_id", "description", "price", "title" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 3, 21, 9, 15, 22, 0, DateTimeKind.Unspecified), 1, "", 12.34m, "Listing A" },
                    { 2, new DateTime(2021, 4, 1, 13, 46, 52, 0, DateTimeKind.Unspecified), 1, "", 22.34m, "Listing B" },
                    { 3, new DateTime(2021, 3, 11, 15, 42, 59, 0, DateTimeKind.Unspecified), 2, "", 32.45m, "Listing C" },
                    { 4, new DateTime(2021, 2, 8, 14, 21, 46, 0, DateTimeKind.Unspecified), 2, "", 455.56m, "Listing D" },
                    { 5, new DateTime(2021, 2, 27, 18, 19, 19, 0, DateTimeKind.Unspecified), 3, "", 556.99m, "Listing E" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "listing",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "listing",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "listing",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "listing",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "listing",
                keyColumn: "id",
                keyValue: 5);
        }
    }
}
