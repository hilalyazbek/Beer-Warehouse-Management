using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Iterates.Bwm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Added_Sales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Beers");

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
                values: new object[] { "Batch #231", new DateTime(2023, 3, 13, 9, 38, 45, 316, DateTimeKind.Local).AddTicks(7559), new DateTime(2023, 3, 13, 9, 38, 45, 316, DateTimeKind.Local).AddTicks(7560) });

            migrationBuilder.UpdateData(
                table: "Brewers",
                keyColumn: "Id",
                keyValue: new Guid("8c641b60-6d70-4fb3-94f0-e8f6c23e8535"),
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2023, 3, 13, 9, 38, 45, 316, DateTimeKind.Local).AddTicks(7423), new DateTime(2023, 3, 13, 9, 38, 45, 316, DateTimeKind.Local).AddTicks(7424) });

            migrationBuilder.UpdateData(
                table: "Brewers",
                keyColumn: "Id",
                keyValue: new Guid("bab4cfe6-e3e9-48c6-9230-8f232a25eda0"),
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2023, 3, 13, 9, 38, 45, 316, DateTimeKind.Local).AddTicks(7394), new DateTime(2023, 3, 13, 9, 38, 45, 316, DateTimeKind.Local).AddTicks(7406) });

            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "Id", "BeerId", "Created", "Delivery", "OrderNumber", "Price", "Stock", "Updated", "WholesalerId" },
                values: new object[] { new Guid("54529c96-ca99-41c0-83c7-b1e10b9037d5"), new Guid("e3fa75d9-82bb-44c8-8ff5-7e3e0ff7f767"), new DateTime(2023, 3, 13, 9, 38, 45, 316, DateTimeKind.Local).AddTicks(7584), true, "#BRU241", 2.20m, 1000, new DateTime(2023, 3, 13, 9, 38, 45, 316, DateTimeKind.Local).AddTicks(7585), new Guid("1847dd70-7b84-4fd7-a611-0e46dbfe0f67") });

            migrationBuilder.UpdateData(
                table: "WholesalerStocks",
                keyColumns: new[] { "BeerId", "WholesalerId" },
                keyValues: new object[] { new Guid("e3fa75d9-82bb-44c8-8ff5-7e3e0ff7f767"), new Guid("1847dd70-7b84-4fd7-a611-0e46dbfe0f67") },
                columns: new[] { "Created", "Id", "Updated" },
                values: new object[] { new DateTime(2023, 3, 13, 9, 38, 45, 316, DateTimeKind.Local).AddTicks(7644), new Guid("305eedb6-9f51-4a44-b0f5-28d28f72d38d"), new DateTime(2023, 3, 13, 9, 38, 45, 316, DateTimeKind.Local).AddTicks(7644) });

            migrationBuilder.UpdateData(
                table: "WholesalerStocks",
                keyColumns: new[] { "BeerId", "WholesalerId" },
                keyValues: new object[] { new Guid("e3fa75d9-82bb-44c8-8ff5-7e3e0ff7f767"), new Guid("4a413b7b-3b8e-457f-b7af-4944b7dd8cda") },
                columns: new[] { "Created", "Id", "Updated" },
                values: new object[] { new DateTime(2023, 3, 13, 9, 38, 45, 316, DateTimeKind.Local).AddTicks(7648), new Guid("bc9051a5-b414-44da-83c6-165e3a3323a1"), new DateTime(2023, 3, 13, 9, 38, 45, 316, DateTimeKind.Local).AddTicks(7648) });

            migrationBuilder.UpdateData(
                table: "Wholesalers",
                keyColumn: "Id",
                keyValue: new Guid("1847dd70-7b84-4fd7-a611-0e46dbfe0f67"),
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2023, 3, 13, 9, 38, 45, 316, DateTimeKind.Local).AddTicks(7606), new DateTime(2023, 3, 13, 9, 38, 45, 316, DateTimeKind.Local).AddTicks(7607) });

            migrationBuilder.UpdateData(
                table: "Wholesalers",
                keyColumn: "Id",
                keyValue: new Guid("4a413b7b-3b8e-457f-b7af-4944b7dd8cda"),
                columns: new[] { "Created", "Updated" },
                values: new object[] { new DateTime(2023, 3, 13, 9, 38, 45, 316, DateTimeKind.Local).AddTicks(7609), new DateTime(2023, 3, 13, 9, 38, 45, 316, DateTimeKind.Local).AddTicks(7610) });

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
        }
    }
}
