using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Core.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoursesEntries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    SystemCourseDate = table.Column<DateTime>(nullable: false),
                    FriendlyCourseDate = table.Column<string>(nullable: true),
                    CourseName = table.Column<string>(nullable: true),
                    CourseLocation = table.Column<string>(nullable: true),
                    CourseShortDescription = table.Column<string>(nullable: true),
                    CourseCurrentCapacity = table.Column<int>(nullable: false),
                    CourseMaxCapacity = table.Column<int>(nullable: false),
                    IsOpened = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesEntries", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursesEntries");
        }
    }
}
