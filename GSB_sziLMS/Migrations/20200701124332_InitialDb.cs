using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GSB_sziLMS.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(nullable: false),
                    Course_Name = table.Column<string>(maxLength: 60, nullable: false),
                    Course_Description = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    Fname = table.Column<string>(maxLength: 60, nullable: false),
                    Lname = table.Column<string>(maxLength: 60, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    AssignmentId = table.Column<Guid>(nullable: false),
                    Assignment_Name = table.Column<string>(maxLength: 60, nullable: false),
                    Assignment_Description = table.Column<string>(maxLength: 60, nullable: false),
                    CourseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.AssignmentId);
                    table.ForeignKey(
                        name: "FK_Assignments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    SectionId = table.Column<Guid>(nullable: false),
                    CourseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.SectionId);
                    table.ForeignKey(
                        name: "FK_Sections_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    EnrollmentId = table.Column<Guid>(nullable: false),
                    SectionId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Role_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.EnrollmentId);
                    table.ForeignKey(
                        name: "FK_Enrollments_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "SectionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    SubmissionId = table.Column<Guid>(nullable: false),
                    AssignmentId = table.Column<Guid>(nullable: false),
                    EnrollmentId = table.Column<Guid>(nullable: false),
                    Score = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.SubmissionId);
                    table.ForeignKey(
                        name: "FK_Submissions_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "AssignmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Submissions_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "EnrollmentId");
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "Course_Description", "Course_Name" },
                values: new object[,]
                {
                    { new Guid("7ead0595-5f85-4134-b687-b24e3436fd33"), "Criminal Methodologies", "Criminology 103" },
                    { new Guid("45c28d41-5c8b-4974-9236-d3776b649295"), "Best Practices for Requirements Gathering.", "Water Analysis" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Fname", "Lname", "Password" },
                values: new object[,]
                {
                    { new Guid("3aaa4e0e-d210-4791-bea4-b847ca737678"), "JJ@gmail.com", "James", "Jamerson", "weonder2039" },
                    { new Guid("e0335745-eb9a-46ca-b70e-fdb0434d54a9"), "KK@gmail.com", "Karen", "Killian", "darir7079" },
                    { new Guid("622dc736-4634-4db8-8bb7-a33f6359a4be"), "LL@gmail.com", "Lama", "Lambada", "weonder2039" }
                });

            migrationBuilder.InsertData(
                table: "Assignments",
                columns: new[] { "AssignmentId", "Assignment_Description", "Assignment_Name", "CourseId" },
                values: new object[,]
                {
                    { new Guid("e0e491c5-83a7-4c22-a86d-e421eac33aaa"), "Define Criminal Behaviour", "Criminology Task 1", new Guid("7ead0595-5f85-4134-b687-b24e3436fd33") },
                    { new Guid("ad44f581-ee0e-4266-94e7-4892196ad9cc"), "List 10 Criminology Methodologies", "Criminology Task 2", new Guid("7ead0595-5f85-4134-b687-b24e3436fd33") }
                });

            migrationBuilder.InsertData(
                table: "Sections",
                columns: new[] { "SectionId", "CourseId" },
                values: new object[,]
                {
                    { new Guid("df925c94-fabd-4184-a362-04c97e8ba1cf"), new Guid("7ead0595-5f85-4134-b687-b24e3436fd33") },
                    { new Guid("b6aad12f-51fe-4bb5-a60d-0ba22f236ab3"), new Guid("7ead0595-5f85-4134-b687-b24e3436fd33") },
                    { new Guid("af4715dd-331a-4229-8444-d78fddbdb256"), new Guid("45c28d41-5c8b-4974-9236-d3776b649295") }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "EnrollmentId", "Role_Id", "SectionId", "UserId" },
                values: new object[] { new Guid("f83d1b8c-a270-4542-bec6-f171f8f3f95c"), 1, new Guid("df925c94-fabd-4184-a362-04c97e8ba1cf"), new Guid("3aaa4e0e-d210-4791-bea4-b847ca737678") });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "EnrollmentId", "Role_Id", "SectionId", "UserId" },
                values: new object[] { new Guid("6bdb643b-cf76-4a6b-8941-bed8a02d0cbd"), 1, new Guid("df925c94-fabd-4184-a362-04c97e8ba1cf"), new Guid("e0335745-eb9a-46ca-b70e-fdb0434d54a9") });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "EnrollmentId", "Role_Id", "SectionId", "UserId" },
                values: new object[] { new Guid("a3ee8728-a3c9-4ab3-b8a1-ea9bff10ed02"), 0, new Guid("df925c94-fabd-4184-a362-04c97e8ba1cf"), new Guid("622dc736-4634-4db8-8bb7-a33f6359a4be") });

            migrationBuilder.InsertData(
                table: "Submissions",
                columns: new[] { "SubmissionId", "AssignmentId", "EnrollmentId", "Score" },
                values: new object[] { new Guid("0b7c5236-9442-4fda-a5db-d621277c5f61"), new Guid("e0e491c5-83a7-4c22-a86d-e421eac33aaa"), new Guid("f83d1b8c-a270-4542-bec6-f171f8f3f95c"), "76%" });

            migrationBuilder.InsertData(
                table: "Submissions",
                columns: new[] { "SubmissionId", "AssignmentId", "EnrollmentId", "Score" },
                values: new object[] { new Guid("55863227-033c-4056-9a89-4fe5a78abe52"), new Guid("ad44f581-ee0e-4266-94e7-4892196ad9cc"), new Guid("f83d1b8c-a270-4542-bec6-f171f8f3f95c"), "80%" });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_CourseId",
                table: "Assignments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_SectionId",
                table: "Enrollments",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_UserId",
                table: "Enrollments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_CourseId",
                table: "Sections",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_AssignmentId",
                table: "Submissions",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_EnrollmentId",
                table: "Submissions",
                column: "EnrollmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Submissions");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
