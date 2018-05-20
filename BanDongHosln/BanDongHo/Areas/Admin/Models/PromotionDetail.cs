using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Domain.DataContext;

namespace BanDongHo.Areas.Admin.Models
{
    public class PromotionDetail
    {
        public IEnumerable<CHITIETKM> getAllPromotionDetail()
        {
            BANDONGHOEntities db = new BANDONGHOEntities();
            return db.CHITIETKMs;
        }
        
        
    }
}