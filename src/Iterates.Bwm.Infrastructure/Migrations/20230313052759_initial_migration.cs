using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Iterates.Bwm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brewers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brewers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wholesalers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wholesalers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Beers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrewerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AlcoholContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beers_Brewers_BrewerId",
                        column: x => x.BrewerId,
                        principalTable: "Brewers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WholesalerStocks",
                columns: table => new
                {
                    WholesalerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BeerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesalerStocks", x => new { x.WholesalerId, x.BeerId });
                    table.ForeignKey(
                        name: "FK_WholesalerStocks_Beers_BeerId",
                        column: x => x.BeerId,
                        principalTable: "Beers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WholesalerStocks_Wholesalers_WholesalerId",
                        column: x => x.WholesalerId,
                        principalTable: "Wholesalers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brewers",
                columns: new[] { "Id", "Created", "Name", "Updated" },
                values: new object[,]
                {
                    { new Guid("8c641b60-6d70-4fb3-94f0-e8f6c23e8535"), new DateTime(2023, 3, 13, 8, 27, 59, 622, DateTimeKind.Local).AddTicks(7972), "Brasserie de la Senne", new DateTime(2023, 3, 13, 8, 27, 59, 622, DateTimeKind.Local).AddTicks(7972) },
                    { new Guid("bab4cfe6-e3e9-48c6-9230-8f232a25eda0"), new DateTime(2023, 3, 13, 8, 27, 59, 622, DateTimeKind.Local).AddTicks(7943), "Abbaye de Leffe", new DateTime(2023, 3, 13, 8, 27, 59, 622, DateTimeKind.Local).AddTicks(7953) }
                });

            migrationBuilder.InsertData(
                table: "Wholesalers",
                columns: new[] { "Id", "Created", "Name", "Updated" },
                values: new object[,]
                {
                    { new Guid("1847dd70-7b84-4fd7-a611-0e46dbfe0f67"), new DateTime(2023, 3, 13, 8, 27, 59, 622, DateTimeKind.Local).AddTicks(8107), "GeneDrinks", new DateTime(2023, 3, 13, 8, 27, 59, 622, DateTimeKind.Local).AddTicks(8108) },
                    { new Guid("4a413b7b-3b8e-457f-b7af-4944b7dd8cda"), new DateTime(2023, 3, 13, 8, 27, 59, 622, DateTimeKind.Local).AddTicks(8111), "OneShot", new DateTime(2023, 3, 13, 8, 27, 59, 622, DateTimeKind.Local).AddTicks(8111) }
                });

            migrationBuilder.InsertData(
                table: "Beers",
                columns: new[] { "Id", "AlcoholContent", "BrewerId", "Created", "Name", "Price", "Updated" },
                values: new object[] { new Guid("e3fa75d9-82bb-44c8-8ff5-7e3e0ff7f767"), "6,6%", new Guid("bab4cfe6-e3e9-48c6-9230-8f232a25eda0"), new DateTime(2023, 3, 13, 8, 27, 59, 622, DateTimeKind.Local).AddTicks(8085), " Leffe Blonde", 2.20m, new DateTime(2023, 3, 13, 8, 27, 59, 622, DateTimeKind.Local).AddTicks(8085) });

            migrationBuilder.InsertData(
                table: "WholesalerStocks",
                columns: new[] { "BeerId", "WholesalerId", "Created", "Id", "Stock", "Updated" },
                values: new object[,]
                {
                    { new Guid("e3fa75d9-82bb-44c8-8ff5-7e3e0ff7f767"), new Guid("1847dd70-7b84-4fd7-a611-0e46dbfe0f67"), new DateTime(2023, 3, 13, 8, 27, 59, 622, DateTimeKind.Local).AddTicks(8129), new Guid("95d54b2e-82a5-4e43-9ab5-1ac7ad3e0140"), 100, new DateTime(2023, 3, 13, 8, 27, 59, 622, DateTimeKind.Local).AddTicks(8129) },
                    { new Guid("e3fa75d9-82bb-44c8-8ff5-7e3e0ff7f767"), new Guid("4a413b7b-3b8e-457f-b7af-4944b7dd8cda"), new DateTime(2023, 3, 13, 8, 27, 59, 622, DateTimeKind.Local).AddTicks(8133), new Guid("c10086fe-37bf-49b3-b8bd-18c8819513c8"), 500, new DateTime(2023, 3, 13, 8, 27, 59, 622, DateTimeKind.Local).AddTicks(8134) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beers_BrewerId",
                table: "Beers",
                column: "BrewerId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesalerStocks_BeerId",
                table: "WholesalerStocks",
                column: "BeerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WholesalerStocks");

            migrationBuilder.DropTable(
                name: "Beers");

            migrationBuilder.DropTable(
                name: "Wholesalers");

            migrationBuilder.DropTable(
                name: "Brewers");
        }
    }
}
