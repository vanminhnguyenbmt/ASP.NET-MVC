using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Domain.DataContext;

namespace BanDongHo.Areas.Admin.Models
{
    public class ProductCategoryService
    {

        BANDONGHOEntities db;

        public ProductCategoryService()
        {
            db = new BANDONGHOEntities();
        }

        public IEnumerable<LOAISANPHAM> getAllProductCategory()
        {        
            return db.LOAISANPHAMs;
        }
    }
}