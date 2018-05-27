using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Domain.DataContext;

namespace BanDongHo.Areas.Admin.Models
{
    public class PromotionDetailService
    {
        BANDONGHOEntities db;

        public PromotionDetailService()
        {
            db = new BANDONGHOEntities();
        }
        public IEnumerable<CHITIETKM> getAllPromotionDetail()
        {           
            return db.CHITIETKMs;
        }
        
        
    }
}