using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CRMSystem.Migrations.OrdersDb
{
    /// <inheritdoc />
    public partial class InitialOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Name", "Text", "TimeStamp", "email" },
                values: new object[,]
                {
                    { new Guid("095e3e31-739e-4715-9c1b-bcb859b78a13"), "Петров", "Заявка на разработку раздела по пожарной безопасности", new DateTime(2024, 7, 19, 12, 45, 43, 975, DateTimeKind.Local).AddTicks(6565), "Petrov@mail.ru" },
                    { new Guid("d7e50d81-97af-44b6-9f51-c8b5b3fb0f8a"), "Иванов", "Заявка на проведение оценки риска", new DateTime(2024, 5, 31, 12, 45, 43, 975, DateTimeKind.Local).AddTicks(6535), "Ivanov@mail.ru" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
