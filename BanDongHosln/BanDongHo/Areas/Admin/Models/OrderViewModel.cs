using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Domain.DataContext;

namespace BanDongHo.Areas.Admin.Models
{
    public class OrderViewModel
    {
        public List<DONHANG> ListOrder { get; set; }
        public List<CHITIETDONHANG> ListOrderDetail { get; set; }
        public List<SANPHAM> ListProduct { get; set; }
        public int TotalPage { get; set; }

        public int mahd { get; set; }
        public int makh { get; set; }
        public String tennguoinhan { get; set; }
        public String sodt { get; set; }
        public String diachi { get; set; }
        public String ngaymua { get; set; }
        public String ngaygiao { get; set; }
        public String tinhtrang { get; set; }
        public int[] mangmasp { get; set; }
        public int[] mangsoluong { get; set; }
        public String[] mangtensp { get; set; }
    }
}