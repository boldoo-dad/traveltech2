using Microsoft.EntityFrameworkCore.Migrations;

namespace traveltech2.Migrations
{
    public partial class init11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FooterID",
                table: "App",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Footer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Footer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FooterIcons",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FooterID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FooterIcons", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FooterIcons_Footer_FooterID",
                        column: x => x.FooterID,
                        principalTable: "Footer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FooterMenus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FooterID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FooterMenus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FooterMenus_Footer_FooterID",
                        column: x => x.FooterID,
                        principalTable: "Footer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FooterMenusLinks",
                columns: table => new
                {
                    FooterMenusId = table.Column<int>(type: "int", nullable: false),
                    LinksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FooterMenusLinks", x => new { x.FooterMenusId, x.LinksId });
                    table.ForeignKey(
                        name: "FK_FooterMenusLinks_FooterMenus_FooterMenusId",
                        column: x => x.FooterMenusId,
                        principalTable: "FooterMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FooterMenusLinks_Links_LinksId",
                        column: x => x.LinksId,
                        principalTable: "Links",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_App_FooterID",
                table: "App",
                column: "FooterID");

            migrationBuilder.CreateIndex(
                name: "IX_FooterIcons_FooterID",
                table: "FooterIcons",
                column: "FooterID");

            migrationBuilder.CreateIndex(
                name: "IX_FooterMenus_FooterID",
                table: "FooterMenus",
                column: "FooterID");

            migrationBuilder.CreateIndex(
                name: "IX_FooterMenusLinks_LinksId",
                table: "FooterMenusLinks",
                column: "LinksId");

            migrationBuilder.AddForeignKey(
                name: "FK_App_Footer_FooterID",
                table: "App",
                column: "FooterID",
                principalTable: "Footer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_App_Footer_FooterID",
                table: "App");

            migrationBuilder.DropTable(
                name: "FooterIcons");

            migrationBuilder.DropTable(
                name: "FooterMenusLinks");

            migrationBuilder.DropTable(
                name: "FooterMenus");

            migrationBuilder.DropTable(
                name: "Footer");

            migrationBuilder.DropIndex(
                name: "IX_App_FooterID",
                table: "App");

            migrationBuilder.DropColumn(
                name: "FooterID",
                table: "App");
        }
    }
}
