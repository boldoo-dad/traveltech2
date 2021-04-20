using Microsoft.EntityFrameworkCore.Migrations;

namespace traveltech2.Migrations
{
    public partial class init12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "FooterIcons",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Head",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Head");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "FooterIcons",
                newName: "ID");
        }
    }
}
