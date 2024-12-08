using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_App.Data
{
    public class DatPhong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaDatPhong { get; set; }
        [Required]
        public DateTime CheckInDate { get; set; }
        [Required]
        public DateTime CheckOutDate { get; set; }
        [Required]
        public int MaKhachHang { get; set; }
        [Required]
        public int MaPhong { get; set; }
        public decimal TongTien { get; set; }
        public Phong phong { get; set; }

        public DatPhong() { }
    }
}
