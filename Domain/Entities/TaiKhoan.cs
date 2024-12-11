using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities;
namespace Domain.Entities
{
    public class TaiKhoan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
