using Microsoft.EntityFrameworkCore.Migrations;

namespace UniMapHHS.Migrations
{
    public partial class ForeignKeyAdd3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Day",
                table: "Times",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Title",
                table: "Categories",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Times_Day",
                table: "Times",
                column: "Day");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Title",
                table: "Categories",
                column: "Title");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Glossaries_Title",
                table: "Categories",
                column: "Title",
                principalTable: "Glossaries",
                principalColumn: "GlossaryId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Times_Glossaries_Day",
                table: "Times",
                column: "Day",
                principalTable: "Glossaries",
                principalColumn: "GlossaryId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Glossaries_Title",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Times_Glossaries_Day",
                table: "Times");

            migrationBuilder.DropIndex(
                name: "IX_Times_Day",
                table: "Times");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Title",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Day",
                table: "Times",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
