﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hotel_App.Data
{
    public class HoaDon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaHoaDon { get; set; }
        public DateTime? NgayTao { get; set; }
        public decimal TongSoTien { get; set; }
        [Required]
        public int MaPhuongThuc { get; set; }
        public int MaTaiKhoan { get; set; }
        [Required]
        public int MaDatPhong { get; set; }
        public int TrangThai { get; set; }


        public ICollection<CTHoaDon> CTHoaDon { get; set; } = []; // One-to-Many with CTHoaDon

        public HoaDon() { }
    }
}