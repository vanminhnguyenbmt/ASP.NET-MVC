using BanDongHo.Domain.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDongHo.Areas.Admin.Models
{
    public class ProductViewModel
    {
        public int MASP { get; set; }
        public string TENSP { get; set; }
        public string HINHLON { get; set; }
        public string HINHNHO { get; set; }
        public string MOTA { get; set; }
        public Nullable<int> MATH { get; set; }
        public string DANHGIA { get; set; }
        public Nullable<int> SOLUONG { get; set; }
        public string MALOAISP { get; set; }
        public Nullable<double> DONGIA { get; set; }

    }
}