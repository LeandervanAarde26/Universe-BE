using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniVerServer.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    CanAccess = table.Column<bool>(type: "boolean", nullable: false),
                    PaidRole = table.Column<bool>(type: "boolean", nullable: false),
                    HourlyRate = table.Column<decimal>(type: "numeric", nullable: false),
                    Identifier = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

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
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    School = table.Column<string>(type: "text", nullable: false),
                    OrganiserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Identifier = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Users_OrganiserId",
                        column: x => x.OrganiserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Cost = table.Column<decimal>(type: "numeric", nullable: false),
                    Credits = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    LecturerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ColorHex = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    ClassRuntime = table.Column<int>(type: "integer", nullable: false),
                    ClassRepitions = table.Column<int>(type: "integer", nullable: false),
                    ClassDayIntervals = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Identifier = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subjects_Users_LecturerId",
                        column: x => x.LecturerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    AcceptingStudents = table.Column<bool>(type: "boolean", nullable: false),
                    Identifier = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Grade = table.Column<decimal>(type: "numeric", nullable: false),
                    GradeType = table.Column<int>(type: "integer", nullable: false),
                    Modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Identifier = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CanAccess", "DateCreated", "HourlyRate", "Identifier", "Name", "PaidRole" },
                values: new object[,]
                {
                    { new Guid("58d25d4c-3b3a-4f52-b282-48e0f458fb79"), false, new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2260), 0.00m, "R1", "Degree", false },
                    { new Guid("68b4d362-9271-4f9f-aa7b-6a9b1953dc3c"), false, new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2270), 20.00m, "R2", "Lecturer", true },
                    { new Guid("a1b2c3d4-e5f6-7a89-b0c1-2d3e4f567890"), true, new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2270), 50.00m, "R3", "Admin", false },
                    { new Guid("a9e3c834-7182-4b3a-80e2-b28a9a7bce42"), false, new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2270), 0.00m, "R4", "Certificate", false }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "ContactNumber", "Credits", "DateCreated", "FirstNames", "Identifier", "IdentityNumber", "IssuedEmail", "LastNames", "ModifiedDate", "Password", "PasswordModifiedDate", "PersonalEmail", "ProfileImage", "RequiredCredits", "RoleId" },
                values: new object[,]
                {
                    { new Guid("14d52b1e-f5b3-4750-b327-56ddadbd1f78"), true, "0825678132", 0, new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2380), "Leander", "200211", "0103265065088", "200211@virtualwindow.co.za", "van Aarde", new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2380), "Password2", new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2380), "Leander.vaonline@gmail.com", "http://example.com/leander1.jpg", 360, new Guid("58d25d4c-3b3a-4f52-b282-48e0f458fb79") },
                    { new Guid("2881e5ba-daad-41f9-92f2-9d67dcc0bd00"), true, "0834567890", 50, new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2380), "Alex", "210312", "0204157890123", "210312@virtualwindow.co.za", "Petterson", new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2380), "Password2", new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2380), "alex.online@gmail.com", "http://example.com/alex1.jpg", 400, new Guid("58d25d4c-3b3a-4f52-b282-48e0f458fb79") },
                    { new Guid("497223a1-720a-408e-8a07-cfa7e48655b3"), true, "0845678901", 100, new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2390), "Jamie", "220513", "0305278901234", "220513@virtualwindow.co.za", "Oliver", new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2380), "Password2", new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2390), "jamie.online@gmail.com", "http://example.com/jamie1.jpg", 500, new Guid("58d25d4c-3b3a-4f52-b282-48e0f458fb79") },
                    { new Guid("49b27cdd-b59f-4a26-80a4-6f7a1c1b2fd7"), true, "0856789012", 360, new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2390), "Taylor", "230614", "0406389012345", "taylor.230614@virtualwindow.co.za", "Swiftee", new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2390), "Password2", new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2390), "taylor.online@gmail.com", "http://example.com/taylor1.jpg", 360, new Guid("68b4d362-9271-4f9f-aa7b-6a9b1953dc3c") },
                    { new Guid("4cfdf887-baf3-4847-a68a-18e7117beddc"), true, "0867890123", 360, new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2390), "Morgan", "240715", "0507490123456", "morgan.240715@virtualwindow.co.za", "Freeman", new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2390), "Password2", new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2390), "morgan.online@gmail.com", "http://example.com/morgan1.jpg", 360, new Guid("68b4d362-9271-4f9f-aa7b-6a9b1953dc3c") },
                    { new Guid("59332d00-80a2-446c-afca-00a3160c0764"), true, "0878901234", 360, new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2400), "Jordan", "250816", "0608501234567", "jordan.250816@virtualwindow.co.za", "Michael", new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2400), "Password2", new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2400), "jordan.online@gmail.com", "http://example.com/jordan1.jpg", 360, new Guid("68b4d362-9271-4f9f-aa7b-6a9b1953dc3c") }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Date", "DateCreated", "Description", "Identifier", "Name", "OrganiserId", "School", "Type" },
                values: new object[,]
                {
                    { new Guid("3f1a17e5-1c8e-4f6b-a5f7-27c5e9d6a41d"), new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2580), new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2580), "Come join us at our open day and explore our winter programs and activities.", "VOID", "Open Day - Winter", new Guid("59332d00-80a2-446c-afca-00a3160c0764"), "CREATIVE TECH", 3 },
                    { new Guid("5d0e2f6c-8d20-4b0a-bf65-77e4ec12b179"), new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2650), new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2650), "Come join us at our open day and learn more about our summer courses and events.", "VOID", "Open Day - Summer", new Guid("59332d00-80a2-446c-afca-00a3160c0764"), "CREATIVE TECH", 3 },
                    { new Guid("a2b92fe1-5918-42f4-957a-0e57c7b3e38b"), new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2650), new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2650), "Come join us at our open day and see what we have planned for the spring semester.", "VOID", "Open Day - Spring", new Guid("59332d00-80a2-446c-afca-00a3160c0764"), "CREATIVE TECH", 3 },
                    { new Guid("b12a9fb8-42cf-4d0a-8a2d-d980dd9ff1e1"), new DateTime(2024, 7, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2670), new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2670), "Come join us at our open day and see what we have in store for the mid-year term.", "VOID", "Open Day - Mid-Year", new Guid("59332d00-80a2-446c-afca-00a3160c0764"), "CREATIVE TECH", 3 },
                    { new Guid("c0840d57-4d73-4f7c-afe6-68b79bc01660"), new DateTime(2024, 9, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2660), new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2660), "Come join us at our open day and discover our autumn curriculum and programs.", "VOID", "Open Day - Autumn", new Guid("59332d00-80a2-446c-afca-00a3160c0764"), "CREATIVE TECH", 3 },
                    { new Guid("e5fe9368-1f62-4d5d-bf98-3c8d12a8764b"), new DateTime(2024, 9, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2660), new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2660), "Come join us at our open day and start the new year with exciting courses and opportunities.", "VOID", "Open Day - New Year", new Guid("59332d00-80a2-446c-afca-00a3160c0764"), "CREATIVE TECH", 3 }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "Active", "ClassDayIntervals", "ClassRepitions", "ClassRuntime", "ColorHex", "Cost", "Credits", "DateCreated", "DateModified", "Description", "Identifier", "Image", "LecturerId", "Name", "Type", "Year" },
                values: new object[,]
                {
                    { new Guid("28384e02-4ac9-4098-89b7-405c465c730e"), true, 7, 8, 120, "#000000", 13000.00m, 60, new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2500), new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2500), "Delve into the world of programming websites with Interactive Development blah blah blah", "IDV100", "http://example.com/interactive-development.jpg", new Guid("59332d00-80a2-446c-afca-00a3160c0764"), "Interactive Development", 2, 1 },
                    { new Guid("2c5240e9-d233-45bb-8d9b-8d6e709b2937"), true, 14, 10, 150, "#FF5733", 15000.00m, 70, new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2510), new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2510), "Learn about the fundamental data structures and algorithms used in computer science blah blah blah", "DSA200", "http://example.com/data-structures.jpg", new Guid("59332d00-80a2-446c-afca-00a3160c0764"), "Data Structures and Algorithms", 2, 2 },
                    { new Guid("2ebc8d73-d02a-4c97-990a-6793dd0c7386"), true, 10, 12, 180, "#33FF57", 17000.00m, 80, new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2510), new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2510), "Explore the concepts and applications of Artificial Intelligence in various fields blah blah blah", "AI304", "http://example.com/ai.jpg", new Guid("59332d00-80a2-446c-afca-00a3160c0764"), "Artificial Intelligence", 1, 3 },
                    { new Guid("2ebd49a2-8d97-4885-832b-8b2f950cbbd3"), true, 12, 9, 130, "#3357FF", 14000.00m, 65, new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2520), new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2520), "Understand the principles of database design and management blah blah blah", "DMS210", "http://example.com/dbms.jpg", new Guid("4cfdf887-baf3-4847-a68a-18e7117beddc"), "Database Management Systems", 2, 2 },
                    { new Guid("37d7ded2-6d83-4b49-ab75-f0bf5209589f"), true, 9, 11, 160, "#FF33A5", 16000.00m, 75, new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2520), new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2520), "Gain insights into the software development lifecycle and methodologies blah blah blah", "SE300", "http://example.com/software-engineering.jpg", new Guid("4cfdf887-baf3-4847-a68a-18e7117beddc"), "Software Engineering", 2, 3 },
                    { new Guid("39fe9dbe-592b-440c-8c80-686f5dd47327"), true, 8, 7, 110, "#33FFB2", 12000.00m, 55, new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2520), new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2520), "Learn the fundamentals of computer networking and protocols blah blah blah", "CNS200", "http://example.com/networks.jpg", new Guid("4cfdf887-baf3-4847-a68a-18e7117beddc"), "Computer Networks", 2, 1 },
                    { new Guid("45a5b291-98f8-4876-860a-99b918a1d24f"), true, 6, 14, 200, "#FF9933", 18000.00m, 85, new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2530), new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2530), "Understand the key concepts and practices in cybersecurity blah blah blah", "CS303", "http://example.com/cybersecurity.jpg", new Guid("59332d00-80a2-446c-afca-00a3160c0764"), "Cybersecurity", 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "AcceptingStudents", "Active", "DateCreated", "EndDate", "Identifier", "StartDate", "SubjectId" },
                values: new object[,]
                {
                    { new Guid("6c058d67-4d10-4fa6-8d6a-4f8b8c5a3a5e"), false, false, new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2740), new DateTime(2025, 1, 26, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2750), "VOID", new DateTime(2024, 9, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2740), new Guid("2c5240e9-d233-45bb-8d9b-8d6e709b2937") },
                    { new Guid("7a3b56c6-1090-4f14-973f-b9b9d4b707cb"), false, false, new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2770), new DateTime(2024, 12, 26, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2780), "VOID", new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2770), new Guid("37d7ded2-6d83-4b49-ab75-f0bf5209589f") },
                    { new Guid("9fcbf95d-89a4-4e19-a828-3baf4aa75ec0"), false, false, new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2750), new DateTime(2024, 12, 26, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2750), "VOID", new DateTime(2024, 9, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2750), new Guid("2ebd49a2-8d97-4885-832b-8b2f950cbbd3") },
                    { new Guid("adff5e28-7a2d-4f71-8c1b-0b6d59a82390"), false, false, new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2770), new DateTime(2025, 1, 26, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2770), "VOID", new DateTime(2024, 9, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2770), new Guid("2ebd49a2-8d97-4885-832b-8b2f950cbbd3") },
                    { new Guid("d3a2f2f0-8e7e-42e1-9f70-61f2a8c0b5ea"), false, false, new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2760), new DateTime(2024, 12, 26, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2760), "VOID", new DateTime(2024, 8, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2760), new Guid("2ebc8d73-d02a-4c97-990a-6793dd0c7386") },
                    { new Guid("f8a259ab-d2f3-482e-9074-842b4a097e27"), false, false, new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2730), new DateTime(2024, 10, 26, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2730), "VOID", new DateTime(2024, 9, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2730), new Guid("28384e02-4ac9-4098-89b7-405c465c730e") }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "Id", "CourseId", "DateCreated", "Grade", "GradeType", "Identifier", "Modified", "Status", "StudentId" },
                values: new object[,]
                {
                    { new Guid("0ae32b37-a1d6-40b7-9d07-e4c3ece96c10"), new Guid("f8a259ab-d2f3-482e-9074-842b4a097e27"), new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2850), 0.00m, 3, "VOID", new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2850), 1, new Guid("14d52b1e-f5b3-4750-b327-56ddadbd1f78") },
                    { new Guid("417dd886-4c89-474c-be39-52e8158a5878"), new Guid("adff5e28-7a2d-4f71-8c1b-0b6d59a82390"), new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2880), 0.00m, 3, "VOID", new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2880), 1, new Guid("2881e5ba-daad-41f9-92f2-9d67dcc0bd00") },
                    { new Guid("a42cead6-a288-425f-903c-092e89b3326f"), new Guid("9fcbf95d-89a4-4e19-a828-3baf4aa75ec0"), new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2870), 0.00m, 3, "VOID", new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2870), 1, new Guid("14d52b1e-f5b3-4750-b327-56ddadbd1f78") },
                    { new Guid("ad9b12db-4f66-4ced-b4c4-33da2d1ba43c"), new Guid("d3a2f2f0-8e7e-42e1-9f70-61f2a8c0b5ea"), new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2870), 0.00m, 3, "VOID", new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2870), 1, new Guid("14d52b1e-f5b3-4750-b327-56ddadbd1f78") },
                    { new Guid("feb3111d-abff-447e-b0b8-2591cd878b04"), new Guid("6c058d67-4d10-4fa6-8d6a-4f8b8c5a3a5e"), new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2860), 0.00m, 3, "VOID", new DateTime(2024, 6, 28, 11, 22, 55, 916, DateTimeKind.Utc).AddTicks(2860), 1, new Guid("14d52b1e-f5b3-4750-b327-56ddadbd1f78") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SubjectId",
                table: "Courses",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseId",
                table: "Enrollments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentId",
                table: "Enrollments",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_OrganiserId",
                table: "Events",
                column: "OrganiserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_LecturerId",
                table: "Subjects",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
