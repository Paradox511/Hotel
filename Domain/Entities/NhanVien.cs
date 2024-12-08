using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities;
namespace Domain.Entities
{
    public class NhanVien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaNhanVien { get; set; }
        [Required]
        public string HoTen { get; set; }
        [Required]
        public string SoDienThoai { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string DiaChi { get; set; }
        //[Required]
        //public int MaTaiKhoan { get; set; }
        [Required]

        public int MaTaiKhoan { get; set; }

        public string Password { get; set; }



        public NhanVien() { }
    }
}
