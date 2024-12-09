using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hotel_App.Data
{
    public class KhachHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaKhachHang { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống.")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Số điện thoại phải chứa 10 chữ số.")]
        public string SoDienThoai { get; set; }

        [Required(ErrorMessage = "Email không được để trống.")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ngày không được để trống.")]
        public DateTime? NgaySinh { get; set; }

        [Required(ErrorMessage = "CCCD không được để trống.")]

        [RegularExpression(@"^\d{12}$", ErrorMessage = "CCCD phải chứa 12 chữ số.")]
        public string CCCD { get; set; }

        [Required(ErrorMessage = "Số tài khoản không được để trống.")]
        public string STK { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        public string Password { get; set; }

        public bool HasReservation { get; set; }
        public KhachHang() { }
    }
}
