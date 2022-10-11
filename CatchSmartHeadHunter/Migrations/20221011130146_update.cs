using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatchSmartHeadHunter.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReqSkills");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReqSkills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PositionId = table.Column<int>(type: "INTEGER", nullable: true),
                    Requirement = table.Column<string>(type: "TEXT", nullable: true),
                    SkillName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReqSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReqSkills_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReqSkills_PositionId",
                table: "ReqSkills",
                column: "PositionId");
        }
    }
}
