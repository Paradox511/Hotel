using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class PhuongThucThanhToan
    {
        [Key]
        public int MaPhuongThuc { get; set; }
        [Required]
        public string TenPhuongThuc { get; set; }
        public PhuongThucThanhToan() { }
    }
}
