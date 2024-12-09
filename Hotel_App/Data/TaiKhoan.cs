using System.ComponentModel.DataAnnotations;
using System;

namespace Hotel_App.Data
{
    public partial class TaiKhoan
    {
        [Key]
        public int MaTaiKhoan { get; set; }
        //[Required]
        //public string Quyen { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        //public int TrangThai { get; set; } = 1;
        //public string? ConfirmPassword { get; set; }

        
    }
}
