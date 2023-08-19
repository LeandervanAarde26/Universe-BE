using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniVerServer.Migrations
{
    /// <inheritdoc />
    public partial class SubjectUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_subjects_people_person_id",
                table: "subjects");

            migrationBuilder.DropIndex(
                name: "IX_subjects_person_id",
                table: "subjects");

            migrationBuilder.RenameColumn(
                name: "person_id",
                table: "subjects",
                newName: "lecturer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lecturer_id",
                table: "subjects",
                newName: "person_id");

            migrationBuilder.CreateIndex(
                name: "IX_subjects_person_id",
                table: "subjects",
                column: "person_id");

            migrationBuilder.AddForeignKey(
                name: "FK_subjects_people_person_id",
                table: "subjects",
                column: "person_id",
                principalTable: "people",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
