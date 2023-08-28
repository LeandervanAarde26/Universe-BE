using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniVerServer.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_events_people_person_system_identifier",
                table: "events");

            migrationBuilder.DropIndex(
                name: "IX_events_person_system_identifier",
                table: "events");

            migrationBuilder.RenameColumn(
                name: "person_system_identifier",
                table: "events",
                newName: "staff_organiser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "staff_organiser",
                table: "events",
                newName: "person_system_identifier");

            migrationBuilder.CreateIndex(
                name: "IX_events_person_system_identifier",
                table: "events",
                column: "person_system_identifier");

            migrationBuilder.AddForeignKey(
                name: "FK_events_people_person_system_identifier",
                table: "events",
                column: "person_system_identifier",
                principalTable: "people",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
