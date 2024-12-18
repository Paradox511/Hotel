﻿using System.ComponentModel.DataAnnotations;

namespace Hotel_Web_App.Data
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
        public DichVu() { }
    }
}
