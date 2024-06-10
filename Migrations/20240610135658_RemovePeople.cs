using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UniVerServer.Migrations
{
    /// <inheritdoc />
    public partial class RemovePeople : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_outstanding_student_fees_people_person_system_identifier",
                table: "outstanding_student_fees");

            migrationBuilder.DropForeignKey(
                name: "FK_student_grades_people_facilitator_id",
                table: "student_grades");

            migrationBuilder.DropForeignKey(
                name: "FK_student_grades_people_student_id",
                table: "student_grades");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_people",
                table: "people");

            migrationBuilder.DropColumn(
                name: "person_id",
                table: "people");

            migrationBuilder.DropColumn(
                name: "first_name",
                table: "people");

            migrationBuilder.DropColumn(
                name: "last_name",
                table: "people");

            migrationBuilder.DropColumn(
                name: "needed_credits",
                table: "people");

            migrationBuilder.DropColumn(
                name: "person_email",
                table: "people");

            migrationBuilder.DropColumn(
                name: "person_system_identifier",
                table: "people");

            migrationBuilder.RenameColumn(
                name: "role",
                table: "people",
                newName: "RequiredCredits");

            migrationBuilder.RenameColumn(
                name: "person_password",
                table: "people",
                newName: "ProfileImage");

            migrationBuilder.RenameColumn(
                name: "person_credits",
                table: "people",
                newName: "Credits");

            migrationBuilder.RenameColumn(
                name: "person_cell",
                table: "people",
                newName: "PersonalEmail");

            migrationBuilder.RenameColumn(
                name: "person_active",
                table: "people",
                newName: "Active");

            migrationBuilder.RenameColumn(
                name: "added_date",
                table: "people",
                newName: "PasswordModifiedDate");

            migrationBuilder.AlterColumn<Guid>(
                name: "student_id",
                table: "student_grades",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<Guid>(
                name: "facilitator_id",
                table: "student_grades",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "people",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ContactNumber",
                table: "people",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "people",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FirstNames",
                table: "people",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Identifier",
                table: "people",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdentityNumber",
                table: "people",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IssuedEmail",
                table: "people",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastNames",
                table: "people",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "people",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "people",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "people",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "person_system_identifier",
                table: "outstanding_student_fees",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_people",
                table: "people",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_people_RoleId",
                table: "people",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_outstanding_student_fees_people_person_system_identifier",
                table: "outstanding_student_fees",
                column: "person_system_identifier",
                principalTable: "people",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_people_people_roles_RoleId",
                table: "people",
                column: "RoleId",
                principalTable: "people_roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_student_grades_people_facilitator_id",
                table: "student_grades",
                column: "facilitator_id",
                principalTable: "people",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_student_grades_people_student_id",
                table: "student_grades",
                column: "student_id",
                principalTable: "people",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_outstanding_student_fees_people_person_system_identifier",
                table: "outstanding_student_fees");

            migrationBuilder.DropForeignKey(
                name: "FK_people_people_roles_RoleId",
                table: "people");

            migrationBuilder.DropForeignKey(
                name: "FK_student_grades_people_facilitator_id",
                table: "student_grades");

            migrationBuilder.DropForeignKey(
                name: "FK_student_grades_people_student_id",
                table: "student_grades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_people",
                table: "people");

            migrationBuilder.DropIndex(
                name: "IX_people_RoleId",
                table: "people");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "people");

            migrationBuilder.DropColumn(
                name: "ContactNumber",
                table: "people");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "people");

            migrationBuilder.DropColumn(
                name: "FirstNames",
                table: "people");

            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "people");

            migrationBuilder.DropColumn(
                name: "IdentityNumber",
                table: "people");

            migrationBuilder.DropColumn(
                name: "IssuedEmail",
                table: "people");

            migrationBuilder.DropColumn(
                name: "LastNames",
                table: "people");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "people");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "people");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "people");

            migrationBuilder.RenameColumn(
                name: "RequiredCredits",
                table: "people",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "ProfileImage",
                table: "people",
                newName: "person_password");

            migrationBuilder.RenameColumn(
                name: "PersonalEmail",
                table: "people",
                newName: "person_cell");

            migrationBuilder.RenameColumn(
                name: "PasswordModifiedDate",
                table: "people",
                newName: "added_date");

            migrationBuilder.RenameColumn(
                name: "Credits",
                table: "people",
                newName: "person_credits");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "people",
                newName: "person_active");

            migrationBuilder.AlterColumn<int>(
                name: "student_id",
                table: "student_grades",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<int>(
                name: "facilitator_id",
                table: "student_grades",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<int>(
                name: "person_id",
                table: "people",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "first_name",
                table: "people",
                type: "character varying(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "last_name",
                table: "people",
                type: "character varying(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "needed_credits",
                table: "people",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "person_email",
                table: "people",
                type: "character varying(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "person_system_identifier",
                table: "people",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "person_system_identifier",
                table: "outstanding_student_fees",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_people",
                table: "people",
                column: "person_id");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    ContactNumber = table.Column<string>(type: "text", nullable: false),
                    Credits = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FirstNames = table.Column<string>(type: "text", nullable: false),
                    Identifier = table.Column<string>(type: "text", nullable: false),
                    IdentityNumber = table.Column<string>(type: "text", nullable: false),
                    IssuedEmail = table.Column<string>(type: "text", nullable: false),
                    LastNames = table.Column<string>(type: "text", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    PasswordModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PersonalEmail = table.Column<string>(type: "text", nullable: false),
                    ProfileImage = table.Column<string>(type: "text", nullable: false),
                    RequiredCredits = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_people_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "people_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_outstanding_student_fees_people_person_system_identifier",
                table: "outstanding_student_fees",
                column: "person_system_identifier",
                principalTable: "people",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_student_grades_people_facilitator_id",
                table: "student_grades",
                column: "facilitator_id",
                principalTable: "people",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_student_grades_people_student_id",
                table: "student_grades",
                column: "student_id",
                principalTable: "people",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
