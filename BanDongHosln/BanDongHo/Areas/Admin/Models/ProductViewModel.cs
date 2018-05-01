using BanDongHo.Domain.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDongHo.Areas.Admin.Models
{
    public class ProductViewModel
    {
        public IEnumerable<SANPHAM> Products { get; set; }
        public Pager Pager { get; set; }

    }

}