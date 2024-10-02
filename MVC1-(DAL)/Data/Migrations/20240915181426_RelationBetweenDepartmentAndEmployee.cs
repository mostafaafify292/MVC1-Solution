using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC1__DAL_.Data.Migrations
{
    public partial class RelationBetweenDepartmentAndEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "employees",
                newName: "id");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_employees_DepartmentId",
                table: "employees",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_employees_departments_DepartmentId",
                table: "employees",
                column: "DepartmentId",
                principalTable: "departments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_departments_DepartmentId",
                table: "employees");

            migrationBuilder.DropIndex(
                name: "IX_employees_DepartmentId",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "employees");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "employees",
                newName: "Id");
        }
    }
}
