using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedacteurPortaal.Data.Migrations
{
    public partial class grain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GrainReferences",
                columns: table => new
                {
                    GrainId = table.Column<string>(type: "text", nullable: false),
                    TypeName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrainReferences", x => x.GrainId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GrainReferences");
        }
    }
}
