using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UniVerServer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_people_roles",
                table: "people_roles");

            migrationBuilder.DropColumn(
                name: "role_id",
                table: "people_roles");

            migrationBuilder.DropColumn(
                name: "rate",
                table: "people_roles");

            migrationBuilder.RenameColumn(
                name: "role_name",
                table: "people_roles",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "can_access",
                table: "people_roles",
                newName: "PaidRole");

            migrationBuilder.AlterColumn<decimal>(
                name: "payment_amount",
                table: "student_payments",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "people_roles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "CanAccess",
                table: "people_roles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "people_roles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "HourlyRate",
                table: "people_roles",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Identifier",
                table: "people_roles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_people_roles",
                table: "people_roles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstNames = table.Column<string>(type: "text", nullable: false),
                    LastNames = table.Column<string>(type: "text", nullable: false),
                    IdentityNumber = table.Column<string>(type: "text", nullable: false),
                    PersonalEmail = table.Column<string>(type: "text", nullable: false),
                    ContactNumber = table.Column<string>(type: "text", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IssuedEmail = table.Column<string>(type: "text", nullable: false),
                    ProfileImage = table.Column<string>(type: "text", nullable: false),
                    Credits = table.Column<int>(type: "integer", nullable: false),
                    RequiredCredits = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    PasswordModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Identifier = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_people_roles",
                table: "people_roles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "people_roles");

            migrationBuilder.DropColumn(
                name: "CanAccess",
                table: "people_roles");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "people_roles");

            migrationBuilder.DropColumn(
                name: "HourlyRate",
                table: "people_roles");

            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "people_roles");

            migrationBuilder.RenameColumn(
                name: "PaidRole",
                table: "people_roles",
                newName: "can_access");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "people_roles",
                newName: "role_name");

            migrationBuilder.AlterColumn<int>(
                name: "payment_amount",
                table: "student_payments",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<int>(
                name: "role_id",
                table: "people_roles",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "rate",
                table: "people_roles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_people_roles",
                table: "people_roles",
                column: "role_id");
        }
    }
}
