using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Domain.DataContext;

namespace BanDongHo.Areas.Admin.Models
{
    public class PromotionService
    {
        BANDONGHOEntities db;

        public PromotionService()
        {
            db = new BANDONGHOEntities();
        }

        public IEnumerable<KHUYENMAI> getAllPromotion()
        {
            db = new BANDONGHOEntities();
            return db.KHUYENMAIs;
        }
    }
}