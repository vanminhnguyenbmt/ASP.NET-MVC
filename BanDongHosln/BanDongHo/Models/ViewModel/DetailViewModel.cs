using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Domain.DataContext;

namespace BanDongHo.Models.ViewModel
{
    public class DetailViewModel
    {
        public SANPHAM Product { get; set; }
        public IEnumerable<SANPHAM> ListProductsRelative { get; set; }
        public string Tag { get; set; }
        public IEnumerable<SANPHAM> ListNewProducts { get; set; }
    }
}