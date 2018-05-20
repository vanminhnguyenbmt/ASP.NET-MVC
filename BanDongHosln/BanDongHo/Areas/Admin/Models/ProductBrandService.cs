using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Domain.DataContext;

namespace BanDongHo.Areas.Admin.Models
{
    public class ProductBrandService
    {
        public IEnumerable<THUONGHIEU> getAllProductBrand()
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            return db.THUONGHIEUx;
        }
    }
}