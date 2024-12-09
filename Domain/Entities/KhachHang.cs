using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class KhachHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaKhachHang { get; set; }
        public string HoTen { get; set; }
        
        public string SoDienThoai { get; set; }

        [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
        public string Email { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string CCCD { get; set; }
        public string STK { get; set; }
        public string Password { get; set; }
        public int Status { get; set; } = 1;
        public KhachHang() { }
    }
}
