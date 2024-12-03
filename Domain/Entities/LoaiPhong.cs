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
        public List <Phong> phong { get; set; } 
        public LoaiPhong() { }
    }
}
