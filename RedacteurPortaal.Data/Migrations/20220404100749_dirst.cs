using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedacteurPortaal.Data.Migrations
{
    public partial class dirst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PluginSettings",
                columns: table => new
                {
                    PluginId = table.Column<string>(type: "text", nullable: false),
                    ApiKey = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PluginSettings", x => x.PluginId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PluginSettings");
        }
    }
}
