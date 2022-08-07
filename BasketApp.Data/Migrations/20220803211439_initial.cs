﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BasketApp.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Stock = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedDate", "Name", "Stock", "UnitPrice" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 8, 3, 21, 14, 39, 764, DateTimeKind.Utc).AddTicks(1805), "Bilgisayar", 20, 100m },
                    { 2, new DateTime(2022, 8, 3, 21, 14, 39, 764, DateTimeKind.Utc).AddTicks(1809), "Buzdolabı", 10, 600m },
                    { 3, new DateTime(2022, 8, 3, 21, 14, 39, 764, DateTimeKind.Utc).AddTicks(1811), "Çamaşır Makinesi", 30, 100m },
                    { 4, new DateTime(2022, 8, 3, 21, 14, 39, 764, DateTimeKind.Utc).AddTicks(1812), "Elektrikli Süpürge", 20, 2500m },
                    { 5, new DateTime(2022, 8, 3, 21, 14, 39, 764, DateTimeKind.Utc).AddTicks(1814), "Bulaşık Makinesi", 40, 6600m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
