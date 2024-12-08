using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class CTHoaDon
    {
        [Key]
        public int Macthd { get; set; }
        public int MaHoaDon { get; set; }
        
        public int MaDichVu { get; set; }
        public int SoLuong { get; set; }
        public decimal TongTien { get; set; }
        public HoaDon HoaDon { get; set; } = null;
        public DichVu dv { get; set; } = null;
    }
}
