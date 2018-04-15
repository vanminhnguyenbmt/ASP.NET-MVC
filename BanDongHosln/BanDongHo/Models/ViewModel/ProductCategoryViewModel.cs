using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Domain.DataContext;

namespace BanDongHo.Models.ViewModel
{
    public class ProductCategoryViewModel
    {
        public List<SANPHAM> ListProduct{ get; set;}
        public int TotalPage { get; set; }
        public int Index { get; set; }
        public int Next
        {
            get
            {
                return Index == TotalPage ? TotalPage : Index + 1;
            }
        }
        public int Prev {
            get
            {
                return Index <= 2 ? 1 : Index - 1;
            }
        }
        public string CateKey { get; set; }
    }
}