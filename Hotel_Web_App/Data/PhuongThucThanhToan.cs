using System.ComponentModel.DataAnnotations;

namespace Hotel_Web_App.Data
{
    public class PhuongThucThanhToan
    {
        [Key]
        public int MaPhuongThuc { get; set; }
        [Required]
        public string TenPhuongThuc { get; set; }
        public PhuongThucThanhToan() { }
    }
}
