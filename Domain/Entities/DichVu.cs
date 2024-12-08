using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class DichVu
    {
        [Key]
        public int MaDichVu { get; set; }
        [Required]
        public string TenDichVu { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Gia { get; set; }
        [Required]
        public string Mota { get; set; }

        public ICollection<CTHoaDon> cthd { get; set; }

        //public ICollection<CTHoaDon> CTHoaDon { get; set; }
        public DichVu() { }
    }
}
