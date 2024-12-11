using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
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
		public int Status { get; set; } = 1;
		[Required]
		public int MaTaiKhoan { get; set; }
		public KhachHang() { }
	}
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
        public int TrangThai { get; set; }

        public KhachHang() { }
    }
}
