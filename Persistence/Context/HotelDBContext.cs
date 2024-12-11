using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
	public partial class HotelDBContext : DbContext, IHotelDBContext
	{

		public HotelDBContext() { }
		public HotelDBContext(DbContextOptions<HotelDBContext> options) : base(options)
		{

		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<CTHoaDon>().HasKey("Macthd");
			modelBuilder.Entity<HoaDon>().HasKey("MaHoaDon");
			modelBuilder.Entity<PhuongThucThanhToan>().HasKey("MaPhuongThuc");
			modelBuilder.Entity<HoaDon>().Property(p => p.TongSoTien).HasColumnType("decimal(10,2)");
			modelBuilder.Entity<LoaiPhong>().Property(p => p.Gia).HasColumnType("decimal(10,2)");
			modelBuilder.Entity<DichVu>().Property(p => p.Gia).HasColumnType("decimal(10,2)");
			modelBuilder.Entity<TaiKhoan>(entity =>
			{
				entity.HasKey(e => e.MaTaiKhoan)
					.HasName("PK_user_id_2")
					.IsClustered(false);

				entity.ToTable("TaiKhoan");

				entity.Property(e => e.MaTaiKhoan).HasColumnName("MaTaiKhoan");

				entity.Property(e => e.Username)
					.IsRequired()
					.HasColumnName("username")
					.HasMaxLength(100)
					.IsUnicode(false);

				entity.Property(e => e.Password)
					.IsRequired()
					.HasColumnName("password")
					.HasMaxLength(100)
					.IsUnicode(false);

            });
  
            modelBuilder.Entity<HoaDon>()
         .HasMany(h => h.CTHoaDon)
         .WithOne(ct => ct.HoaDon)
         .HasForeignKey(ct => ct.MaHoaDon);


            modelBuilder.Entity<CTHoaDon>()
             .HasOne(ct => ct.HoaDon)
            .WithMany(h => h.CTHoaDon)
             .HasForeignKey(ct => ct.MaHoaDon);

			modelBuilder.Entity<CTHoaDon>()
				.HasOne(ct => ct.dv)
				.WithMany(dv => dv.cthds)
				.HasForeignKey(ct => ct.MaDichVu);

            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<HoaDon> hoadon { get; set; }
        public virtual DbSet<CTHoaDon> cthoadon { get; set; }
        public virtual DbSet<Phong> phong { get; set; }
        public virtual DbSet<DatPhong> datphong { get; set; }
        public virtual DbSet<LoaiPhong> loaiphong { get; set; }
        public virtual DbSet<DichVu> dichvu { get; set; }
        public virtual DbSet<KhachHang> khachhang { get; set; }
        public virtual DbSet<TaiKhoan> taikhoan { get; set; }

        public virtual DbSet<PhuongThucThanhToan> ptthanhtoan { get; set; }

		public async Task<int> SaveChangesAsync()
		{
			return await base.SaveChangesAsync();
		}

	}

}
