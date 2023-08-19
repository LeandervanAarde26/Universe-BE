using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniVerServer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEnrollments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_course_enrollments_subjects_subject_id",
                table: "course_enrollments");

            migrationBuilder.DropIndex(
                name: "IX_course_enrollments_subject_id",
                table: "course_enrollments");

            migrationBuilder.RenameColumn(
                name: "subject_id",
                table: "course_enrollments",
                newName: "Subjects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Subjects",
                table: "course_enrollments",
                newName: "subject_id");

            migrationBuilder.CreateIndex(
                name: "IX_course_enrollments_subject_id",
                table: "course_enrollments",
                column: "subject_id");

            migrationBuilder.AddForeignKey(
                name: "FK_course_enrollments_subjects_subject_id",
                table: "course_enrollments",
                column: "subject_id",
                principalTable: "subjects",
                principalColumn: "subject_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
