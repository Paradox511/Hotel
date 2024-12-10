using System.ComponentModel.DataAnnotations;

namespace Hotel_App.Data
{
    public class Phong
    {
        [Key]
        public int MaPhong { get; set; }
        public int TrangThaiPhong { get; set; } = 1;
        [Required(ErrorMessage = "Số phòng không được để trống.")]
		[RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Số phòng phải là số nguyên.")]
		public int SoPhong { get; set; }
        [Required]
        public int MaLoaiPhong { get; set; }

		public int TrangThai { get; set; } = 1;

		public LoaiPhong? LoaiPhong { get; set; }

		public Phong() { }
    }
}
