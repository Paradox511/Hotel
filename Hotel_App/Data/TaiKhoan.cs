using System.ComponentModel.DataAnnotations;
using Hotel_App.Data;
namespace Hotel_App.Data
{
    public class TaiKhoan
    {
        [Key]
        public int MaTaiKhoan { get; set; }
        [Required]
        public string HoTen { get; set; }
        [Required]
        public string SoDienThoai { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string DiaChi { get; set; }
      
        public string Username {  get; set; }
        [Required]
        public string Password { get; set; }
        public int TrangThai { get; set; }


        public TaiKhoan() { }
    }
}
