using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IHotelDBContext
    {
        DbSet<HoaDon> hoadon { get; set; }
        DbSet<CTHoaDon> cthoadon { get; set; }
        DbSet<Phong> phong { get; set; }
        DbSet<DatPhong> datphong { get; set; }
        DbSet<LoaiPhong> loaiphong { get; set; }
        DbSet<DichVu> dichvu { get; set; }
        DbSet<KhachHang> khachhang { get; set; }
        DbSet<NhanVien> nhanvien { get; set; }

        DbSet<TaiKhoan> taikhoan { get; set; }
        DbSet<PhuongThucThanhToan> ptthanhtoan { get; set; }
    }
}
