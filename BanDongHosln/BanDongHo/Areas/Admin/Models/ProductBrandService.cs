using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Domain.DataContext;

namespace BanDongHo.Areas.Admin.Models
{
   
    public class ProductBrandService
    {
        BANDONGHOEntities db;

        public ProductBrandService()
        {
            db = new BANDONGHOEntities();
        }

        public IEnumerable<THUONGHIEU> getAllProductBrand()
        {          
            return db.THUONGHIEUx;
        }
    }
}