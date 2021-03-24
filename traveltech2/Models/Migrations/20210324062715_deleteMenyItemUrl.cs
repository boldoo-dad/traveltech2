using Microsoft.EntityFrameworkCore.Migrations;

namespace traveltech2.Migrations
{
    public partial class deleteMenyItemUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Heads_MenuItems_MenuItemID",
                table: "Heads");

            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Urls_UrlID",
                table: "Menus");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "Urls");

            migrationBuilder.DropIndex(
                name: "IX_Menus_UrlID",
                table: "Menus");

            migrationBuilder.DropIndex(
                name: "IX_Heads_MenuItemID",
                table: "Heads");

            migrationBuilder.DropColumn(
                name: "UrlID",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "MenuItemID",
                table: "Heads");

            migrationBuilder.AddColumn<int>(
                name: "DropID",
                table: "Menus",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Menus",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Menus");

            migrationBuilder.AddColumn<int>(
                name: "UrlID",
                table: "Menus",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MenuItemID",
                table: "Heads",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DropID = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItems_Drops_DropID",
                        column: x => x.DropID,
                        principalTable: "Drops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Urls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urls", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Menus_UrlID",
                table: "Menus",
                column: "UrlID");

            migrationBuilder.CreateIndex(
                name: "IX_Heads_MenuItemID",
                table: "Heads",
                column: "MenuItemID");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_DropID",
                table: "MenuItems",
                column: "DropID");

            migrationBuilder.AddForeignKey(
                name: "FK_Heads_MenuItems_MenuItemID",
                table: "Heads",
                column: "MenuItemID",
                principalTable: "MenuItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Urls_UrlID",
                table: "Menus",
                column: "UrlID",
                principalTable: "Urls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
