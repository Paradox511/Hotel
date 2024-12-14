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

		[EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
		public string Email { get; set; }
		public DateTime? NgaySinh { get; set; }
		public string CCCD { get; set; }
		public string STK { get; set; }
		public int TrangThai { get; set; }

		public KhachHang() { }
	}
}