using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLiNhanVien.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceReviews_Users_UserId",
                table: "PerformanceReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Salary_Users_UserId",
                table: "Salary");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkSchedules_Users_UserId",
                table: "WorkSchedules");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "WorkSchedules",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkSchedules_UserId",
                table: "WorkSchedules",
                newName: "IX_WorkSchedules_EmployeeId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Salary",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Salary_UserId",
                table: "Salary",
                newName: "IX_Salary_EmployeeId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "PerformanceReviews",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_PerformanceReviews_UserId",
                table: "PerformanceReviews",
                newName: "IX_PerformanceReviews_EmployeeId");

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceReviews_Employees_EmployeeId",
                table: "PerformanceReviews",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Salary_Employees_EmployeeId",
                table: "Salary",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSchedules_Employees_EmployeeId",
                table: "WorkSchedules",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceReviews_Employees_EmployeeId",
                table: "PerformanceReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Salary_Employees_EmployeeId",
                table: "Salary");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkSchedules_Employees_EmployeeId",
                table: "WorkSchedules");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "WorkSchedules",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkSchedules_EmployeeId",
                table: "WorkSchedules",
                newName: "IX_WorkSchedules_UserId");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Salary",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Salary_EmployeeId",
                table: "Salary",
                newName: "IX_Salary_UserId");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "PerformanceReviews",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PerformanceReviews_EmployeeId",
                table: "PerformanceReviews",
                newName: "IX_PerformanceReviews_UserId");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceReviews_Users_UserId",
                table: "PerformanceReviews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Salary_Users_UserId",
                table: "Salary",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSchedules_Users_UserId",
                table: "WorkSchedules",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
