using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BillPaymentSystem.Migrations
{
    /// <inheritdoc />
    public partial class migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriberNo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriberId = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_Subscribers_SubscriberId",
                        column: x => x.SubscriberId,
                        principalTable: "Subscribers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Subscribers",
                columns: new[] { "Id", "SubscriberNo" },
                values: new object[,]
                {
                    { 1, "10" },
                    { 2, "20" },
                    { 3, "30" },
                    { 4, "40" },
                    { 5, "50" },
                    { 6, "60" },
                    { 7, "70" }
                });

            migrationBuilder.InsertData(
                table: "Bills",
                columns: new[] { "Id", "IsPaid", "Month", "SubscriberId", "TotalAmount" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 260m },
                    { 2, false, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 260m },
                    { 3, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 260m },
                    { 4, false, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 260m },
                    { 5, false, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 260m },
                    { 6, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 260m },
                    { 7, false, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 260m },
                    { 8, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 260m },
                    { 9, false, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 260m },
                    { 10, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 260m },
                    { 11, false, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 260m },
                    { 12, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 260m },
                    { 13, false, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 260m },
                    { 14, true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 260m },
                    { 15, true, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 260m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_SubscriberId",
                table: "Bills",
                column: "SubscriberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Subscribers");
        }
    }
}
