using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UniMapHHS.Migrations
{
    public partial class FixIoT3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Times",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Times",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Times",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.DropForeignKey(
                name: "FK_Times_Glossaries_Day",
                table: "Times");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Times",
                table: "Times");

            migrationBuilder.DropIndex(
                name: "IX_Times_Day",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "TimeId",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "Hour",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "Test",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "Week",
                table: "Times");

            migrationBuilder.AddColumn<int>(
                name: "HistoryId",
                table: "Times",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Times",
                table: "Times",
                column: "HistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Times_LocationId",
                table: "Times",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Times_Locations_LocationId",
                table: "Times",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Times_Locations_LocationId",
                table: "Times");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Times",
                table: "Times");

            migrationBuilder.DropIndex(
                name: "IX_Times_LocationId",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "HistoryId",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Times");

            migrationBuilder.AddColumn<int>(
                name: "TimeId",
                table: "Times",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "Times",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Hour",
                table: "Times",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Test",
                table: "Times",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Week",
                table: "Times",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Times",
                table: "Times",
                column: "TimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Times_Day",
                table: "Times",
                column: "Day");

            migrationBuilder.AddForeignKey(
                name: "FK_Times_Glossaries_Day",
                table: "Times",
                column: "Day",
                principalTable: "Glossaries",
                principalColumn: "GlossaryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
