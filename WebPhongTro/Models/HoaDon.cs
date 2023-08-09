using System;
using System.Collections.Generic;

namespace WebPhongTro.Models
{
    public partial class HoaDon
    {
        public int IdHoadon { get; set; }
        public int? IdHopdong { get; set; }
        public DateTime? NgayLap { get; set; }
        public int? TienDien { get; set; }
        public int? TienNuoc { get; set; }
        public int? TongTien { get; set; }

        public virtual HopDong? IdHopdongNavigation { get; set; }
    }
}
