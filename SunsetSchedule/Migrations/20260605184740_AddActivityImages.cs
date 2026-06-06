using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SunsetSchedule.Migrations
{
    /// <inheritdoc />
    public partial class AddActivityImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainImagePath",
                table: "Activities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ActivityImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ActivityId = table.Column<int>(type: "INTEGER", nullable: false),
                    ImagePath = table.Column<string>(type: "TEXT", nullable: false),
                    Caption = table.Column<string>(type: "TEXT", nullable: true),
                    DisplayOrder = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityImages_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityImages_ActivityId",
                table: "ActivityImages",
                column: "ActivityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityImages");

            migrationBuilder.DropColumn(
                name: "MainImagePath",
                table: "Activities");
        }
    }
}
