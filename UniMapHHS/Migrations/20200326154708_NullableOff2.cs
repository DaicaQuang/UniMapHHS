using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniMapHHS.Migrations
{
    public partial class NullableOff2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Histories");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeStamp",
                table: "Histories",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "Histories");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Histories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
