using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Exercise1.DbScaffold.Migrations.VirbelaListing
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "region",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(unicode: false, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_region", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "listinguser",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userid = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    firstname = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    lastname = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    password = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    region_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_listinguser", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_region",
                        column: x => x.region_id,
                        principalTable: "region",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "listing",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    description = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    price = table.Column<decimal>(type: "money", nullable: false),
                    creator_id = table.Column<int>(nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_listing", x => x.id);
                    table.ForeignKey(
                        name: "FK_listing_creator_user",
                        column: x => x.creator_id,
                        principalTable: "listinguser",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_listing_creator_id",
                table: "listing",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "IX_listinguser_region_id",
                table: "listinguser",
                column: "region_id");

            migrationBuilder.CreateIndex(
                name: "IX_userid",
                table: "listinguser",
                column: "userid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "listing");

            migrationBuilder.DropTable(
                name: "listinguser");

            migrationBuilder.DropTable(
                name: "region");
        }
    }
}
