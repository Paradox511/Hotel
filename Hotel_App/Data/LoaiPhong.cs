using System.ComponentModel.DataAnnotations;

namespace Hotel_App.Data
{
	public class LoaiPhong
	{
		[Key]
		public int MaLoaiPhong { get; set; }
		[Required(ErrorMessage = "Tên loại phòng không được để trống.")]
		public string TenLoaiPhong { get; set; }
		[Required(ErrorMessage = "Số lượng phòng không được để trống.")]
		[RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Số lượng phòng phải là số nguyên lớn hơn 0.")]
		public int SoLuongPhong { get; set; }
		[Required(ErrorMessage = "Mô tả không được để trống.")]
		public string Mota { get; set; }
		[Required(ErrorMessage = "Giá phòng không được để trống.")]
		[DataType(DataType.Currency)]

		[Range(100000, (double)decimal.MaxValue, ErrorMessage = "Giá phòng phải là số nguyên ít nhất là 100000.")]
		public decimal Gia { get; set; }
		public LoaiPhong() { }

		public int TrangThai { get; set; } = 1;

		public List<Phong> phong { get; set; }
	}
}