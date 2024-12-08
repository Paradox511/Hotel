using System.ComponentModel.DataAnnotations;

namespace Hotel_App.Data
{
    public class CTHoaDon
    {
        
        public int Macthd { get; set; }
        public int MaHoaDon { get; set; }
        
        public int SoLuong { get; set; }
        public decimal TongTien { get; set; }
        public int MaDichVu { get; set; }
        public DichVu dv { get; set; } = null;
     //   public HoaDon hd { get; set; } = null;

    }
}
