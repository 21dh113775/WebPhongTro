using System;
using System.Collections.Generic;

namespace WebPhongTro.Models
{
    public partial class VatDung
    {
        public int IdVatdung { get; set; }
        public string? TenVatdung { get; set; }
        public int? IdPhong { get; set; }

        public virtual Phong? IdPhongNavigation { get; set; }
    }
}
