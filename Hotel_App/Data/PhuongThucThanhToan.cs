using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_App.Data
{
	[Table("ptthanhtoan")]
	public class PhuongThucThanhToan
	{
		[Key]
		public int MaPhuongThuc { get; set; }
		[Required]
		public string TenPhuongThuc { get; set; }
		public PhuongThucThanhToan() { }
	}
}
