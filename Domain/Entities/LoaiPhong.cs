using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
	public class LoaiPhong
	{
		[Key]
		public int MaLoaiPhong { get; set; }
		[Required]
		public string TenLoaiPhong { get; set; }
		[Required]
		public int SoLuongPhong { get; set; }
		[Required]
		public string Mota { get; set; }
		[Required]
		[DataType(DataType.Currency)]
		public decimal Gia { get; set; }
		[Required]
		public ICollection<Phong> phong { get; set; }

		public int TrangThai { get; set; } = 1;
		public LoaiPhong() { }
	}
}