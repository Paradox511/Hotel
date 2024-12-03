using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Phong
    {
        [Key]
		
		public int MaPhong { get; set; }
        public int TrangThaiPhong { get; set; } = 1;
      
        [Required(ErrorMessage = "Số Phòng không được để trống.")]
        public int SoPhong { get; set; }
        [Required]
        public int MaLoaiPhong { get; set; }
        public LoaiPhong LoaiPhong { get; set; }
       
        public Phong() { }
    }
}
