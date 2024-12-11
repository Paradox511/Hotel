using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_phong_loaiphong_LoaiPhongMaLoaiPhong",
                table: "phong");

            migrationBuilder.DropIndex(
                name: "IX_phong_LoaiPhongMaLoaiPhong",
                table: "phong");

            migrationBuilder.DropColumn(
                name: "LoaiPhongMaLoaiPhong",
                table: "phong");

            migrationBuilder.CreateIndex(
                name: "IX_phong_MaLoaiPhong",
                table: "phong",
                column: "MaLoaiPhong");

            migrationBuilder.AddForeignKey(
                name: "FK_phong_loaiphong_MaLoaiPhong",
                table: "phong",
                column: "MaLoaiPhong",
                principalTable: "loaiphong",
                principalColumn: "MaLoaiPhong",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_phong_loaiphong_MaLoaiPhong",
                table: "phong");

            migrationBuilder.DropIndex(
                name: "IX_phong_MaLoaiPhong",
                table: "phong");

            migrationBuilder.AddColumn<int>(
                name: "LoaiPhongMaLoaiPhong",
                table: "phong",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_phong_LoaiPhongMaLoaiPhong",
                table: "phong",
                column: "LoaiPhongMaLoaiPhong");

            migrationBuilder.AddForeignKey(
                name: "FK_phong_loaiphong_LoaiPhongMaLoaiPhong",
                table: "phong",
                column: "LoaiPhongMaLoaiPhong",
                principalTable: "loaiphong",
                principalColumn: "MaLoaiPhong",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
