using Microsoft.EntityFrameworkCore.Migrations;

namespace traveltech2.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apps_Heads_HeadID",
                table: "Apps");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Menus_MenuID",
                table: "MenuItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Heads_HeadID",
                table: "Menus");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_MenuID",
                table: "MenuItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Heads",
                table: "Heads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Apps",
                table: "Apps");

            migrationBuilder.DropColumn(
                name: "MenuID",
                table: "MenuItems");

            migrationBuilder.RenameTable(
                name: "Heads",
                newName: "Head");

            migrationBuilder.RenameTable(
                name: "Apps",
                newName: "App");

            migrationBuilder.RenameIndex(
                name: "IX_Apps_HeadID",
                table: "App",
                newName: "IX_App_HeadID");

            migrationBuilder.AddColumn<int>(
                name: "MenusID",
                table: "MenuItems",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Head",
                table: "Head",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_App",
                table: "App",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_MenusID",
                table: "MenuItems",
                column: "MenusID");

            migrationBuilder.AddForeignKey(
                name: "FK_App_Head_HeadID",
                table: "App",
                column: "HeadID",
                principalTable: "Head",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Menus_MenusID",
                table: "MenuItems",
                column: "MenusID",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Head_HeadID",
                table: "Menus",
                column: "HeadID",
                principalTable: "Head",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_App_Head_HeadID",
                table: "App");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Menus_MenusID",
                table: "MenuItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Head_HeadID",
                table: "Menus");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_MenusID",
                table: "MenuItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Head",
                table: "Head");

            migrationBuilder.DropPrimaryKey(
                name: "PK_App",
                table: "App");

            migrationBuilder.DropColumn(
                name: "MenusID",
                table: "MenuItems");

            migrationBuilder.RenameTable(
                name: "Head",
                newName: "Heads");

            migrationBuilder.RenameTable(
                name: "App",
                newName: "Apps");

            migrationBuilder.RenameIndex(
                name: "IX_App_HeadID",
                table: "Apps",
                newName: "IX_Apps_HeadID");

            migrationBuilder.AddColumn<int>(
                name: "MenuID",
                table: "MenuItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Heads",
                table: "Heads",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Apps",
                table: "Apps",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_MenuID",
                table: "MenuItems",
                column: "MenuID");

            migrationBuilder.AddForeignKey(
                name: "FK_Apps_Heads_HeadID",
                table: "Apps",
                column: "HeadID",
                principalTable: "Heads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Menus_MenuID",
                table: "MenuItems",
                column: "MenuID",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Heads_HeadID",
                table: "Menus",
                column: "HeadID",
                principalTable: "Heads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
