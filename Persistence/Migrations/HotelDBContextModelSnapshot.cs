﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.Context;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(HotelDBContext))]
    partial class HotelDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.CTHoaDon", b =>
                {
                    b.Property<int>("Macthd")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Macthd"));

                    b.Property<int>("MaDichVu")
                        .HasColumnType("int");

                    b.Property<int>("MaHoaDon")
                        .HasColumnType("int");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<decimal>("TongTien")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Macthd");

                    b.HasIndex("MaDichVu");

                    b.HasIndex("MaHoaDon");

                    b.ToTable("cthoadon");
                });

            modelBuilder.Entity("Domain.Entities.DatPhong", b =>
                {
                    b.Property<int>("MaDatPhong")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaDatPhong"));

                    b.Property<DateTime>("CheckInDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CheckOutDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MaKhachHang")
                        .HasColumnType("int");

                    b.Property<int>("MaPhong")
                        .HasColumnType("int");

                    b.Property<decimal?>("TongTien")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("MaDatPhong");

                    b.ToTable("datphong");
                });

            modelBuilder.Entity("Domain.Entities.DichVu", b =>
                {
                    b.Property<int>("MaDichVu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaDichVu"));

                    b.Property<decimal>("Gia")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Mota")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenDichVu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaDichVu");

                    b.ToTable("dichvu");
                });

            modelBuilder.Entity("Domain.Entities.HoaDon", b =>
                {
                    b.Property<int>("MaHoaDon")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaHoaDon"));

                    b.Property<int>("MaDatPhong")
                        .HasColumnType("int");

                    b.Property<int>("MaNhanVien")
                        .HasColumnType("int");

                    b.Property<int>("MaPhuongThuc")
                        .HasColumnType("int");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("TongSoTien")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("MaHoaDon");

                    b.ToTable("hoadon");
                });

            modelBuilder.Entity("Domain.Entities.KhachHang", b =>
                {
                    b.Property<int>("MaKhachHang")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaKhachHang"));

                    b.Property<string>("CCCD")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaTaiKhoan")
                        .HasColumnType("int");

                    b.Property<DateTime?>("NgaySinh")
                        .HasColumnType("datetime2");

                    b.Property<string>("STK")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoDienThoai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaKhachHang");

                    b.ToTable("khachhang");
                });

            modelBuilder.Entity("Domain.Entities.LoaiPhong", b =>
                {
                    b.Property<int>("MaLoaiPhong")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaLoaiPhong"));

                    b.Property<decimal>("Gia")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Mota")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SoLuongPhong")
                        .HasColumnType("int");

                    b.Property<string>("TenLoaiPhong")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaLoaiPhong");

                    b.ToTable("loaiphong");
                });

            modelBuilder.Entity("Domain.Entities.NhanVien", b =>
                {
                    b.Property<int>("MaNhanVien")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaNhanVien"));

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoDienThoai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaNhanVien");

                    b.ToTable("nhanvien");
                });

            modelBuilder.Entity("Domain.Entities.Phong", b =>
                {
                    b.Property<int>("MaPhong")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaPhong"));

                    b.Property<int>("MaLoaiPhong")
                        .HasColumnType("int");

                    b.Property<int>("SoPhong")
                        .HasColumnType("int");

                    b.Property<int>("TrangThaiPhong")
                        .HasColumnType("int");

                    b.HasKey("MaPhong");

                    b.HasIndex("MaLoaiPhong");

                    b.ToTable("phong");
                });

            modelBuilder.Entity("Domain.Entities.PhuongThucThanhToan", b =>
                {
                    b.Property<int>("MaPhuongThuc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaPhuongThuc"));

                    b.Property<string>("TenPhuongThuc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaPhuongThuc");

                    b.ToTable("ptthanhtoan");
                });

            modelBuilder.Entity("Domain.Entities.TaiKhoan", b =>
                {
                    b.Property<int>("MaTaiKhoan")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MaTaiKhoan");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaTaiKhoan"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("password");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("username");

                    b.HasKey("MaTaiKhoan")
                        .HasName("PK_user_id_2");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("MaTaiKhoan"), false);

                    b.ToTable("TaiKhoan", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.CTHoaDon", b =>
                {
                    b.HasOne("Domain.Entities.DichVu", "dv")
                        .WithMany("cthds")
                        .HasForeignKey("MaDichVu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.HoaDon", "HoaDon")
                        .WithMany("CTHoaDon")
                        .HasForeignKey("MaHoaDon")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HoaDon");

                    b.Navigation("dv");
                });

            modelBuilder.Entity("Domain.Entities.Phong", b =>
                {
                    b.HasOne("Domain.Entities.LoaiPhong", "LoaiPhong")
                        .WithMany()
                        .HasForeignKey("MaLoaiPhong")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LoaiPhong");
                });

            modelBuilder.Entity("Domain.Entities.DichVu", b =>
                {
                    b.Navigation("cthds");
                });

            modelBuilder.Entity("Domain.Entities.HoaDon", b =>
                {
                    b.Navigation("CTHoaDon");
                });
#pragma warning restore 612, 618
        }
    }
}
