using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniVerServer.Migrations
{
    /// <inheritdoc />
    public partial class ChangedtosubjectsisActiveandDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "course_start",
                table: "course_enrollments");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "course_enrollments");

            migrationBuilder.AddColumn<DateTime>(
                name: "course_start",
                table: "subjects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "subjects",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "course_start",
                table: "subjects");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "subjects");

            migrationBuilder.AddColumn<DateTime>(
                name: "course_start",
                table: "course_enrollments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "course_enrollments",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
