using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Domain.DataContext;

namespace BanDongHo.Areas.Admin.Models
{
    public class OrderService
    {
        BANDONGHOEntities db;

        public OrderService()
        {
            db = new BANDONGHOEntities();
        }
        public IEnumerable<DONHANG> getAllOrder()
        {          
            return db.DONHANGs;
        }
    }
}