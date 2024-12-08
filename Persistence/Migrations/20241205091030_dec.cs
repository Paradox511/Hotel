using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class dec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaTaiKhoan",
                table: "nhanvien");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "nhanvien",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TongTien",
                table: "datphong",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoLuong",
                table: "cthoadon",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TongTien",
                table: "cthoadon",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "nhanvien");

            migrationBuilder.DropColumn(
                name: "TongTien",
                table: "datphong");

            migrationBuilder.DropColumn(
                name: "SoLuong",
                table: "cthoadon");

            migrationBuilder.DropColumn(
                name: "TongTien",
                table: "cthoadon");

            migrationBuilder.AddColumn<int>(
                name: "MaTaiKhoan",
                table: "nhanvien",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
