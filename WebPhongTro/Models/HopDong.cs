using System;
using System.Collections.Generic;

namespace WebPhongTro.Models
{
    public partial class HopDong
    {
        public HopDong()
        {
            HoaDons = new HashSet<HoaDon>();
            IdUsers = new HashSet<AspNetUser>();
        }

        public int IdHopdong { get; set; }
        public int? IdPhong { get; set; }
        public int? IdKhach { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }

        public virtual Phong? IdPhongNavigation { get; set; }
        public virtual ICollection<HoaDon> HoaDons { get; set; }

        public virtual ICollection<AspNetUser> IdUsers { get; set; }
    }
}
