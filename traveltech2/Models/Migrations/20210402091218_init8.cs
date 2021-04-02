using Microsoft.EntityFrameworkCore.Migrations;

namespace traveltech2.Migrations
{
    public partial class init8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_MenuItems_MenuItemsId",
                table: "Links");

            migrationBuilder.DropIndex(
                name: "IX_Links_MenuItemsId",
                table: "Links");

            migrationBuilder.DropColumn(
                name: "MenuItemsId",
                table: "Links");

            migrationBuilder.CreateTable(
                name: "LinksMenuItems",
                columns: table => new
                {
                    LinksId = table.Column<int>(type: "int", nullable: false),
                    MenuItemsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinksMenuItems", x => new { x.LinksId, x.MenuItemsId });
                    table.ForeignKey(
                        name: "FK_LinksMenuItems_Links_LinksId",
                        column: x => x.LinksId,
                        principalTable: "Links",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LinksMenuItems_MenuItems_MenuItemsId",
                        column: x => x.MenuItemsId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinksMenuItems_MenuItemsId",
                table: "LinksMenuItems",
                column: "MenuItemsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinksMenuItems");

            migrationBuilder.AddColumn<int>(
                name: "MenuItemsId",
                table: "Links",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Links_MenuItemsId",
                table: "Links",
                column: "MenuItemsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Links_MenuItems_MenuItemsId",
                table: "Links",
                column: "MenuItemsId",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
