using Microsoft.EntityFrameworkCore.Migrations;

namespace traveltech2.Migrations
{
    public partial class OneMenuManyDropRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Drops_DropID",
                table: "Menus");

            migrationBuilder.DropIndex(
                name: "IX_Menus_DropID",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "DropID",
                table: "Menus");

            migrationBuilder.AddColumn<int>(
                name: "MenuID",
                table: "Drops",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drops_MenuID",
                table: "Drops",
                column: "MenuID");

            migrationBuilder.AddForeignKey(
                name: "FK_Drops_Menus_MenuID",
                table: "Drops",
                column: "MenuID",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drops_Menus_MenuID",
                table: "Drops");

            migrationBuilder.DropIndex(
                name: "IX_Drops_MenuID",
                table: "Drops");

            migrationBuilder.DropColumn(
                name: "MenuID",
                table: "Drops");

            migrationBuilder.AddColumn<int>(
                name: "DropID",
                table: "Menus",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Menus_DropID",
                table: "Menus",
                column: "DropID");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Drops_DropID",
                table: "Menus",
                column: "DropID",
                principalTable: "Drops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
