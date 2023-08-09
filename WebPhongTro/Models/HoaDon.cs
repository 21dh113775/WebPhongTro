using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebPhongTro.Models
{
    public partial class HoaDon
    {
        [Display(Name ="Mã Hóa Đơn")]
        public int IdHoadon { get; set; }
        [Display(Name = "Mã Hợp Đồng")]
        public int? IdHopdong { get; set; }
        [Display(Name = "Ngày Lập")]
        public DateTime? NgayLap { get; set; }
        [Display(Name = "Tiền Điện")]
        public int? TienDien { get; set; }
        [Display(Name = "Tiền Nước")]
        public int? TienNuoc { get; set; }
        [Display(Name = "Tổng Tiền")]
        public int? TongTien { get; set; }
        
        public virtual HopDong? IdHopdongNavigation { get; set; }
    }
}
