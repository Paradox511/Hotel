using System.ComponentModel.DataAnnotations;
using Hotel_App.Data;
namespace Hotel_App.Data
{
    public class TaiKhoan
    {
        [Key]
        public int MaTaiKhoan { get; set; }
        [Required(ErrorMessage = "Họ tên không được để trống.")]
        public string HoTen { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        public string SoDienThoai { get; set; }
        [Required(ErrorMessage = "Email không được để trống.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được để trống.")]
        public string DiaChi { get; set; }
        [Required(ErrorMessage = "Username không được để trống.")]
        public string Username {  get; set; }
        [Required(ErrorMessage = "Password không được để trống.")]
        public string Password { get; set; }
        public int TrangThai { get; set; } = 1;


        public TaiKhoan() { }
    }
}
