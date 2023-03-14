using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Iterates.Bwm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Added_Sale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beers_Brewers_BrewerId",
                table: "Beers");

            migrationBuilder.DropIndex(
                name: "IX_Beers_BrewerId",
                table: "Beers");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Beers");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "WholesalerStocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "BatchNumber",
                table: "Beers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrewerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WholesalerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BeerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Delivery = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_Beers_BeerId",
                        column: x => x.BeerId,
                        principalTable: "Beers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_Wholesalers_WholesalerId",
                        column: x => x.WholesalerId,
                        principalTable: "Wholesalers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: new Guid("e3fa75d9-82bb-44c8-8ff5-7e3e0ff7f767"),
                columns: new[] { "BatchNumber", "Created", "Updated" },
                values: new object[] { "Batch #231", new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1800), new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1800) });

            migrationBuilder.UpdateData(
                table: "Brewers",
                keyColumn: "Id",
                keyValue: new Guid("8c641b60-6d70-4fb3-94f0-e8f6c23e8535"),
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1700), new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1700) });

            migrationBuilder.UpdateData(
                table: "Brewers",
                keyColumn: "Id",
                keyValue: new Guid("bab4cfe6-e3e9-48c6-9230-8f232a25eda0"),
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1630), new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1660) });

            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "Id", "BeerId", "BrewerId", "Created", "Delivery", "OrderNumber", "Price", "Stock", "Updated", "WholesalerId" },
                values: new object[] { new Guid("499e866c-d0fd-4c5e-9856-6720c350c714"), new Guid("e3fa75d9-82bb-44c8-8ff5-7e3e0ff7f767"), new Guid("bab4cfe6-e3e9-48c6-9230-8f232a25eda0"), new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1840), true, "#BRU241", 2.20m, 1000, new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1840), new Guid("1847dd70-7b84-4fd7-a611-0e46dbfe0f67") });

            migrationBuilder.UpdateData(
                table: "WholesalerStocks",
                keyColumns: new[] { "BeerId", "WholesalerId" },
                keyValues: new object[] { new Guid("e3fa75d9-82bb-44c8-8ff5-7e3e0ff7f767"), new Guid("1847dd70-7b84-4fd7-a611-0e46dbfe0f67") },
                columns: new[] { "Created", "Id", "Price", "Updated" },
                values: new object[] { new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1920), new Guid("78c128c3-0ebd-464d-8b2b-7bbac3b17604"), 2.2m, new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1920) });

            migrationBuilder.UpdateData(
                table: "WholesalerStocks",
                keyColumns: new[] { "BeerId", "WholesalerId" },
                keyValues: new object[] { new Guid("e3fa75d9-82bb-44c8-8ff5-7e3e0ff7f767"), new Guid("4a413b7b-3b8e-457f-b7af-4944b7dd8cda") },
                columns: new[] { "Created", "Id", "Price", "Updated" },
                values: new object[] { new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1940), new Guid("21e93c96-e2b0-4f02-af10-fbd31555b983"), 2.3m, new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1940) });

            migrationBuilder.UpdateData(
                table: "Wholesalers",
                keyColumn: "Id",
                keyValue: new Guid("1847dd70-7b84-4fd7-a611-0e46dbfe0f67"),
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1880), new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1880) });

            migrationBuilder.UpdateData(
                table: "Wholesalers",
                keyColumn: "Id",
                keyValue: new Guid("4a413b7b-3b8e-457f-b7af-4944b7dd8cda"),
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1890), new DateTime(2023, 3, 14, 21, 19, 34, 704, DateTimeKind.Local).AddTicks(1890) });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_BeerId",
                table: "Sales",
                column: "BeerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_WholesalerId",
                table: "Sales",
                column: "WholesalerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "WholesalerStocks");

            migrationBuilder.DropColumn(
                name: "BatchNumber",
                table: "Beers");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Beers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: new Guid("e3fa75d9-82bb-44c8-8ff5-7e3e0ff7f767"),
                columns: new[] { "Created", "Price", "Updated" },
                values: new object[] { new DateTime(2023, 3, 13, 8, 27, 59, 622, DateTimeKind.Local).AddTicks(8085), 2.20m, new DateTime(2023, 3, 13, 8, 27, 59, 622, DateTimeKind.Local).AddTicks(8085) });

            migrationBuilder.UpdateData(
                table: "Brewers",
                keyColumn: "Id",
                keyValue: new Guid("8c641b60-6d70-4fb3-94f0-e8f6c23e8535"),
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2023, 3, 13, 8, 27, 59, 622, DateTimeKind.Local).AddTicks(7972), new DateTime(2023, 3, 13, 8, 27, 59, 622, DateTimeKind.Local).AddTicks(7972) });

            migrationBuilder.UpdateData(
                table: "Brewers",
                keyColumn: "Id",
                keyValue: new Guid("bab4cfe6-e3e9-48c6-9230-8f232a25eda0"),
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2023, 3, 13, 8, 27, 59, 622, DateTimeKind.Local).AddTicks(7943), new DateTime(2023, 3, 13, 8, 27, 59, 622, DateTimeKind.Local).AddTicks(7953) });

            migrationBuilder.UpdateData(
                table: "WholesalerStocks",
                keyColumns: new[] { "BeerId", "WholesalerId" },
                keyValues: new object[] { new Guid("e3fa75d9-82bb-44c8-8ff5-7e3e0ff7f767"), new Guid("1847dd70-7b84-4fd7-a611-0e46dbfe0f67") },
                columns: new[] { "Created", "Id", "Updated" },
                values: new object[] { new DateTime(2023, 3, 13, 8, 27, 59, 622, DateTimeKind.Local).AddTicks(8129), new Guid("95d54b2e-82a5-4e43-9ab5-1ac7ad3e0140"), new DateTime(2023, 3, 13, 8, 27, 59, 622, DateTimeKind.Local).AddTicks(8129) });

            migrationBuilder.UpdateData(
                table: "WholesalerStocks",
                keyColumns: new[] { "BeerId", "WholesalerId" },
                keyValues: new object[] { new Guid("e3fa75d9-82bb-44c8-8ff5-7e3e0ff7f767"), new Guid("4a413b7b-3b8e-457f-b7af-4944b7dd8cda") },
                columns: new[] { "Created", "Id", "Updated" },
                values: new object[] { new DateTime(2023, 3, 13, 8, 27, 59, 622, DateTimeKind.Local).AddTicks(8133), new Guid("c10086fe-37bf-49b3-b8bd-18c8819513c8"), new DateTime(2023, 3, 13, 8, 27, 59, 622, DateTimeKind.Local).AddTicks(8134) });

            migrationBuilder.UpdateData(
                table: "Wholesalers",
                keyColumn: "Id",
                keyValue: new Guid("1847dd70-7b84-4fd7-a611-0e46dbfe0f67"),
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2023, 3, 13, 8, 27, 59, 622, DateTimeKind.Local).AddTicks(8107), new DateTime(2023, 3, 13, 8, 27, 59, 622, DateTimeKind.Local).AddTicks(8108) });

            migrationBuilder.UpdateData(
                table: "Wholesalers",
                keyColumn: "Id",
                keyValue: new Guid("4a413b7b-3b8e-457f-b7af-4944b7dd8cda"),
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2023, 3, 13, 8, 27, 59, 622, DateTimeKind.Local).AddTicks(8111), new DateTime(2023, 3, 13, 8, 27, 59, 622, DateTimeKind.Local).AddTicks(8111) });

            migrationBuilder.CreateIndex(
                name: "IX_Beers_BrewerId",
                table: "Beers",
                column: "BrewerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beers_Brewers_BrewerId",
                table: "Beers",
                column: "BrewerId",
                principalTable: "Brewers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
