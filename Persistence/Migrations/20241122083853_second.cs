using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "cthdMacthd",
                table: "dichvu",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Macthd",
                table: "cthoadon",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cthoadon",
                table: "cthoadon",
                column: "Macthd");

            migrationBuilder.CreateIndex(
                name: "IX_dichvu_cthdMacthd",
                table: "dichvu",
                column: "cthdMacthd");

            migrationBuilder.CreateIndex(
                name: "IX_cthoadon_MaDichVu",
                table: "cthoadon",
                column: "MaDichVu");

            migrationBuilder.CreateIndex(
                name: "IX_cthoadon_MaHoaDon",
                table: "cthoadon",
                column: "MaHoaDon");

            migrationBuilder.AddForeignKey(
                name: "FK_cthoadon_dichvu_MaDichVu",
                table: "cthoadon",
                column: "MaDichVu",
                principalTable: "dichvu",
                principalColumn: "MaDichVu",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cthoadon_hoadon_MaHoaDon",
                table: "cthoadon",
                column: "MaHoaDon",
                principalTable: "hoadon",
                principalColumn: "MaHoaDon",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dichvu_cthoadon_cthdMacthd",
                table: "dichvu",
                column: "cthdMacthd",
                principalTable: "cthoadon",
                principalColumn: "Macthd",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cthoadon_dichvu_MaDichVu",
                table: "cthoadon");

            migrationBuilder.DropForeignKey(
                name: "FK_cthoadon_hoadon_MaHoaDon",
                table: "cthoadon");

            migrationBuilder.DropForeignKey(
                name: "FK_dichvu_cthoadon_cthdMacthd",
                table: "dichvu");

            migrationBuilder.DropIndex(
                name: "IX_dichvu_cthdMacthd",
                table: "dichvu");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cthoadon",
                table: "cthoadon");

            migrationBuilder.DropIndex(
                name: "IX_cthoadon_MaDichVu",
                table: "cthoadon");

            migrationBuilder.DropIndex(
                name: "IX_cthoadon_MaHoaDon",
                table: "cthoadon");

            migrationBuilder.DropColumn(
                name: "cthdMacthd",
                table: "dichvu");

            migrationBuilder.DropColumn(
                name: "Macthd",
                table: "cthoadon");
        }
    }
}
