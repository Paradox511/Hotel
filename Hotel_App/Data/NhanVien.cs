using System.ComponentModel.DataAnnotations;
using Hotel_App.Data;
namespace Hotel_App.Data
{
    public class NhanVien
    {
        [Key]
        public int MaNhanVien { get; set; }
        [Required]
        public string HoTen { get; set; }
        [Required]
        public string SoDienThoai { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string DiaChi { get; set; }
      
        [Required]
        public string Password { get; set; }

        public NhanVien() { }
    }
}
