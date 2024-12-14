using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "datphong",
                columns: table => new
                {
                    MaDatPhong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaKhachHang = table.Column<int>(type: "int", nullable: false),
                    MaPhong = table.Column<int>(type: "int", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_datphong", x => x.MaDatPhong);
                });

            migrationBuilder.CreateTable(
                name: "dichvu",
                columns: table => new
                {
                    MaDichVu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDichVu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gia = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dichvu", x => x.MaDichVu);
                });

            migrationBuilder.CreateTable(
                name: "hoadon",
                columns: table => new
                {
                    MaHoaDon = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TongSoTien = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    MaPhuongThuc = table.Column<int>(type: "int", nullable: false),
                    MaTaiKhoan = table.Column<int>(type: "int", nullable: false),
                    MaDatPhong = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hoadon", x => x.MaHoaDon);
                });

            migrationBuilder.CreateTable(
                name: "khachhang",
                columns: table => new
                {
                    MaKhachHang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CCCD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    STK = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_khachhang", x => x.MaKhachHang);
                });

            migrationBuilder.CreateTable(
                name: "loaiphong",
                columns: table => new
                {
                    MaLoaiPhong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoaiPhong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuongPhong = table.Column<int>(type: "int", nullable: false),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gia = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loaiphong", x => x.MaLoaiPhong);
                });

            migrationBuilder.CreateTable(
                name: "PhuongThucThanhToan",
                columns: table => new
                {
                    MaPhuongThuc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenPhuongThuc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhuongThucThanhToan", x => x.MaPhuongThuc);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoan",
                columns: table => new
                {
                    MaTaiKhoan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    username = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_id_2", x => x.MaTaiKhoan)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "cthoadon",
                columns: table => new
                {
                    Macthd = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHoaDon = table.Column<int>(type: "int", nullable: false),
                    MaDichVu = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cthoadon", x => x.Macthd);
                    table.ForeignKey(
                        name: "FK_cthoadon_dichvu_MaDichVu",
                        column: x => x.MaDichVu,
                        principalTable: "dichvu",
                        principalColumn: "MaDichVu",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cthoadon_hoadon_MaHoaDon",
                        column: x => x.MaHoaDon,
                        principalTable: "hoadon",
                        principalColumn: "MaHoaDon",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "phong",
                columns: table => new
                {
                    MaPhong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrangThaiPhong = table.Column<int>(type: "int", nullable: false),
                    SoPhong = table.Column<int>(type: "int", nullable: false),
                    MaLoaiPhong = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    LoaiPhongMaLoaiPhong = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phong", x => x.MaPhong);
                    table.ForeignKey(
                        name: "FK_phong_loaiphong_LoaiPhongMaLoaiPhong",
                        column: x => x.LoaiPhongMaLoaiPhong,
                        principalTable: "loaiphong",
                        principalColumn: "MaLoaiPhong");
                    table.ForeignKey(
                        name: "FK_phong_loaiphong_MaLoaiPhong",
                        column: x => x.MaLoaiPhong,
                        principalTable: "loaiphong",
                        principalColumn: "MaLoaiPhong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cthoadon_MaDichVu",
                table: "cthoadon",
                column: "MaDichVu");

            migrationBuilder.CreateIndex(
                name: "IX_cthoadon_MaHoaDon",
                table: "cthoadon",
                column: "MaHoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_phong_LoaiPhongMaLoaiPhong",
                table: "phong",
                column: "LoaiPhongMaLoaiPhong");

            migrationBuilder.CreateIndex(
                name: "IX_phong_MaLoaiPhong",
                table: "phong",
                column: "MaLoaiPhong");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cthoadon");

            migrationBuilder.DropTable(
                name: "datphong");

            migrationBuilder.DropTable(
                name: "khachhang");

            migrationBuilder.DropTable(
                name: "phong");

            migrationBuilder.DropTable(
                name: "PhuongThucThanhToan");

            migrationBuilder.DropTable(
                name: "TaiKhoan");

            migrationBuilder.DropTable(
                name: "dichvu");

            migrationBuilder.DropTable(
                name: "hoadon");

            migrationBuilder.DropTable(
                name: "loaiphong");
        }
    }
}
