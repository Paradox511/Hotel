using System.ComponentModel.DataAnnotations;

namespace Hotel_App.Data
{
    public class LoaiPhong
    {
        [Key]
        public int MaLoaiPhong { get; set; }
        [Required]
        public string TenLoaiPhong { get; set; }
        [Required]
        public int SoLuongPhong { get; set; }
        [Required]
        public string Mota { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Gia { get; set; }
        public LoaiPhong() { }
    }
}
