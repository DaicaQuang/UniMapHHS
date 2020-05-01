using Microsoft.EntityFrameworkCore.Migrations;

namespace UniMapHHS.Migrations
{
    public partial class UpdatedGlossary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Spanish",
                table: "Glossaries",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Spanish",
                table: "Glossaries");
        }
    }
}
