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
                    { new Guid("3934dc31-17b2-4174-8d0a-be032fb9a602"), "Петров", "Заявка на разработку раздела по пожарной безопасности", new DateTime(2024, 7, 18, 15, 39, 1, 100, DateTimeKind.Local).AddTicks(3172), "Petrov@mail.ru" },
                    { new Guid("addbbfc7-da09-42b6-a5e4-f1ce1604f58e"), "Иванов", "Заявка на проведение оценки риска", new DateTime(2024, 5, 30, 15, 39, 1, 100, DateTimeKind.Local).AddTicks(3139), "Ivanov@mail.ru" }
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
