using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Domain.DataContext;

namespace BanDongHo.Areas.Admin.Models
{
    public class OrderService
    {
        public IEnumerable<DONHANG> getAllOrder()
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            return db.DONHANGs;
        }
    }
}