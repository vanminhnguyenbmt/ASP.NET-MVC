using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Domain.DataContext;

namespace BanDongHo.Areas.Admin.Models
{
    public class ProductCategoryService
    {
        public IEnumerable<LOAISANPHAM> getAllProductCategory()
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            return db.LOAISANPHAMs;
        }
    }
}