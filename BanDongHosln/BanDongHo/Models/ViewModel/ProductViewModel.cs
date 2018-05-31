using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Domain.DataContext;


namespace BanDongHo.Models.ViewModel
{
    public class ProductViewModel
    {
        public SANPHAM Product { get; set; }
        public int Promotion { get; set; }
    }
}