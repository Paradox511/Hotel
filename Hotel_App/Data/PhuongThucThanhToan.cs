using System.ComponentModel.DataAnnotations;

namespace Hotel_App.Data
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
