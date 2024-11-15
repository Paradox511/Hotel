﻿using Domain.Entities;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{ 
    public class HotelDBContext : DbContext,IHotelDBContext
    {
        public HotelDBContext(DbContextOptions<HotelDBContext> options) : base(options) {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CTHoaDon>().HasNoKey();
            modelBuilder.Entity<HoaDon>().HasKey("MaHoaDon");
            modelBuilder.Entity<HoaDon>().Property(p => p.TongSoTien).HasColumnType("decimal(10,2)");
            modelBuilder.Entity<LoaiPhong>().Property(p => p.Gia).HasColumnType("decimal(10,2)");
            modelBuilder.Entity<DichVu>().Property(p => p.Gia).HasColumnType("decimal(10,2)");



            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<HoaDon> hoadon { get; set; }
        public virtual DbSet<CTHoaDon> cthoadon { get; set; }
        public virtual DbSet<Phong> phong { get; set; }
        public virtual DbSet<DatPhong> datphong { get; set; }
        public virtual DbSet<LoaiPhong> loaiphong { get; set; }
        public virtual DbSet<DichVu> dichvu { get; set; }
        public virtual DbSet<KhachHang> khachhang { get; set; }
        public virtual DbSet<NhanVien> nhanvien { get; set; }

        public virtual DbSet<TaiKhoan> taikhoan { get; set; }
        public virtual DbSet<PhuongThucThanhToan> ptthanhtoan { get; set; }
    }

}
