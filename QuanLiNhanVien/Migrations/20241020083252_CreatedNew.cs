using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLiNhanVien.Migrations
{
    /// <inheritdoc />
    public partial class CreatedNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HourlyRate",
                table: "Salary");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HourlyRate",
                table: "Salary",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
