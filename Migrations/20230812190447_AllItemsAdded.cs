using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UniVerServer.Migrations
{
    /// <inheritdoc />
    public partial class AllItemsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "people_address",
                columns: table => new
                {
                    address_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    address = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    address_province = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    address_city = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    address_area = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    address_zipcode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    address_phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_people_address", x => x.address_id);
                });

            migrationBuilder.CreateTable(
                name: "people_roles",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    can_access = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_people_roles", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "people",
                columns: table => new
                {
                    person_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    person_system_identifier = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    first_name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    last_name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    person_email = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    added_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    person_active = table.Column<bool>(type: "boolean", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    address_id = table.Column<int>(type: "integer", nullable: false),
                    person_credits = table.Column<int>(type: "integer", nullable: false),
                    person_password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_people", x => x.person_id);
                    table.ForeignKey(
                        name: "FK_people_people_address_address_id",
                        column: x => x.address_id,
                        principalTable: "people_address",
                        principalColumn: "address_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_people_people_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "people_roles",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    event_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    event_name = table.Column<string>(type: "text", nullable: false),
                    event_description = table.Column<string>(type: "text", nullable: false),
                    person_system_identifier = table.Column<int>(type: "integer", nullable: false),
                    event_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.event_id);
                    table.ForeignKey(
                        name: "FK_events_people_person_system_identifier",
                        column: x => x.person_system_identifier,
                        principalTable: "people",
                        principalColumn: "person_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "outstanding_student_fees",
                columns: table => new
                {
                    fee_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    person_system_identifier = table.Column<int>(type: "integer", nullable: false),
                    outstanding_amount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_outstanding_student_fees", x => x.fee_id);
                    table.ForeignKey(
                        name: "FK_outstanding_student_fees_people_person_system_identifier",
                        column: x => x.person_system_identifier,
                        principalTable: "people",
                        principalColumn: "person_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "student_payments",
                columns: table => new
                {
                    payment_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    person_system_identifier = table.Column<int>(type: "integer", nullable: false),
                    payment_amount = table.Column<int>(type: "integer", nullable: false),
                    payment_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student_payments", x => x.payment_id);
                    table.ForeignKey(
                        name: "FK_student_payments_people_person_system_identifier",
                        column: x => x.person_system_identifier,
                        principalTable: "people",
                        principalColumn: "person_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subjects",
                columns: table => new
                {
                    subject_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    subject_name = table.Column<string>(type: "text", nullable: false),
                    subject_code = table.Column<string>(type: "text", nullable: false),
                    subject_description = table.Column<string>(type: "text", nullable: false),
                    subject_cost = table.Column<int>(type: "integer", nullable: false),
                    subject_color = table.Column<string>(type: "text", nullable: false),
                    person_id = table.Column<int>(type: "integer", nullable: false),
                    subject_credits = table.Column<int>(type: "integer", nullable: false),
                    subject_class_runtiem = table.Column<int>(type: "integer", nullable: false),
                    subject_class_amount = table.Column<int>(type: "integer", nullable: false),
                    subjectImage = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjects", x => x.subject_id);
                    table.ForeignKey(
                        name: "FK_subjects_people_person_id",
                        column: x => x.person_id,
                        principalTable: "people",
                        principalColumn: "person_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "course_enrollments",
                columns: table => new
                {
                    enrollment_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    student_id = table.Column<string>(type: "text", nullable: false),
                    course_start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    subject_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course_enrollments", x => x.enrollment_id);
                    table.ForeignKey(
                        name: "FK_course_enrollments_subjects_subject_id",
                        column: x => x.subject_id,
                        principalTable: "subjects",
                        principalColumn: "subject_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "student_grades",
                columns: table => new
                {
                    grade_id = table.Column<string>(type: "text", nullable: false),
                    subject_id = table.Column<int>(type: "integer", nullable: false),
                    student_id = table.Column<int>(type: "integer", nullable: false),
                    facilitator_id = table.Column<int>(type: "integer", nullable: false),
                    grade = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student_grades", x => x.grade_id);
                    table.ForeignKey(
                        name: "FK_student_grades_people_facilitator_id",
                        column: x => x.facilitator_id,
                        principalTable: "people",
                        principalColumn: "person_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_student_grades_people_student_id",
                        column: x => x.student_id,
                        principalTable: "people",
                        principalColumn: "person_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_student_grades_subjects_subject_id",
                        column: x => x.subject_id,
                        principalTable: "subjects",
                        principalColumn: "subject_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_course_enrollments_subject_id",
                table: "course_enrollments",
                column: "subject_id");

            migrationBuilder.CreateIndex(
                name: "IX_events_person_system_identifier",
                table: "events",
                column: "person_system_identifier");

            migrationBuilder.CreateIndex(
                name: "IX_outstanding_student_fees_person_system_identifier",
                table: "outstanding_student_fees",
                column: "person_system_identifier");

            migrationBuilder.CreateIndex(
                name: "IX_people_address_id",
                table: "people",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "IX_people_role_id",
                table: "people",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_student_grades_facilitator_id",
                table: "student_grades",
                column: "facilitator_id");

            migrationBuilder.CreateIndex(
                name: "IX_student_grades_student_id",
                table: "student_grades",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_student_grades_subject_id",
                table: "student_grades",
                column: "subject_id");

            migrationBuilder.CreateIndex(
                name: "IX_student_payments_person_system_identifier",
                table: "student_payments",
                column: "person_system_identifier");

            migrationBuilder.CreateIndex(
                name: "IX_subjects_person_id",
                table: "subjects",
                column: "person_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "course_enrollments");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "outstanding_student_fees");

            migrationBuilder.DropTable(
                name: "student_grades");

            migrationBuilder.DropTable(
                name: "student_payments");

            migrationBuilder.DropTable(
                name: "subjects");

            migrationBuilder.DropTable(
                name: "people");

            migrationBuilder.DropTable(
                name: "people_address");

            migrationBuilder.DropTable(
                name: "people_roles");
        }
    }
}
