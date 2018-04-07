using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Domain.DataContext;

namespace BanDongHo.Models.ViewModel
{
    public class HomePageViewModel
    {
        public IEnumerable<SANPHAM> ProductsSelling { get; set; }
        public IEnumerable<SANPHAM> NewProducts { get; set; }
    }
}