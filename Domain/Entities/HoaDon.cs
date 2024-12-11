using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class HoaDon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaHoaDon { get; set; }
        public DateTime? NgayTao { get; set; }
        public decimal? TongSoTien { get; set; }
        [Required]
        public int MaPhuongThuc { get; set; }
        public int MaTaiKhoan { get; set; }
        [Required]
        public int MaDatPhong { get; set; }
        public ICollection<CTHoaDon> CTHoaDon { get; set; } // One-to-Many with CTHoaDon
        public int TrangThai { get; set; }

        public HoaDon() { }
    }
}