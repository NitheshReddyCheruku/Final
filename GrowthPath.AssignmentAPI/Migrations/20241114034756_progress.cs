using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GRowthPath.AssignmentAPI.Migrations
{
    /// <inheritdoc />
    public partial class progress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModulesCompleted",
                table: "CourseAssignments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModulesCompleted",
                table: "CourseAssignments");
        }
    }
}
