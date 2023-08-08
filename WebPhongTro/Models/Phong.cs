using System;
using System.Collections.Generic;

namespace WebPhongTro.Models
{
    public partial class Phong
    {
        public Phong()
        {

            HopDongs = new HashSet<HopDong>();
            VatDungs = new HashSet<VatDung>();
        }

        public int IdPhong { get; set; }
        public string? TenPhong { get; set; }
        public int? DienTich { get; set; }
        public double? GiaPhong { get; set; }
        public string? TrangThai { get; set; }
        public string? HinhAnh { get; set; }

        public virtual ICollection<HopDong> HopDongs { get; set; }
        public virtual ICollection<VatDung> VatDungs { get; set; }
    }
}
