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
		[RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Số lượng phòng phải là số nguyên.")]
		public int SoLuongPhong { get; set; }
		[Required(ErrorMessage = "Mô tả không được để trống.")]
		public string Mota { get; set; }
        [Required(ErrorMessage = "Giá phòng không được để trống.")]
        [DataType(DataType.Currency)]

		[RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Giá phòng phải là số nguyên.")]
		public decimal Gia { get; set; }
        public LoaiPhong() { }

		public List<Phong> phong { get; set; }
	}
}
