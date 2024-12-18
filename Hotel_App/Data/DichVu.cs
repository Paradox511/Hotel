﻿using System.ComponentModel.DataAnnotations;

namespace Hotel_App.Data
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
        public DichVu() { }
        public int TrangThai { get; set; }


        [Required]
        public string Mota { get; set; }
        public IEnumerable<CTHoaDon>? cthds { get; set; } = [];

    }
}
