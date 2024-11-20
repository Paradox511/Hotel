using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hotel_Web_App.Data
{
    public class KhachHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaKhachHang { get; set; }
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string CCCD { get; set; }
        public string STK { get; set; }
        [Required]
        public int MaTaiKhoan { get; set; }
        public KhachHang() { }
    }
}
