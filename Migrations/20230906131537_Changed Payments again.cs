using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniVerServer.Migrations
{
    /// <inheritdoc />
    public partial class ChangedPaymentsagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_student_payments_people_person_system_identifier",
                table: "student_payments");

            migrationBuilder.DropIndex(
                name: "IX_student_payments_person_system_identifier",
                table: "student_payments");

            migrationBuilder.RenameColumn(
                name: "person_system_identifier",
                table: "student_payments",
                newName: "person_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "person_id",
                table: "student_payments",
                newName: "person_system_identifier");

            migrationBuilder.CreateIndex(
                name: "IX_student_payments_person_system_identifier",
                table: "student_payments",
                column: "person_system_identifier");

            migrationBuilder.AddForeignKey(
                name: "FK_student_payments_people_person_system_identifier",
                table: "student_payments",
                column: "person_system_identifier",
                principalTable: "people",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
