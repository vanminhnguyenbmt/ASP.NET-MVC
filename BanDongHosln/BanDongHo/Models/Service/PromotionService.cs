using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDongHo.Domain.DataContext;

namespace BanDongHo.Models.Service
{
    public class PromotionService
    {
        public static int GetPromotion(int id)
        {
            int result = 0;
            using (var db = new BANDONGHOEntities())
            {
                List<int> listValue = (from ct in db.CHITIETKMs
                                       join km in db.KHUYENMAIs on ct.MAKM equals km.MAKM
                                       where ct.MASP == id &&
                                       DateTime.Compare(km.NGAYBD.Value, DateTime.Today) <= 0 &&
                                       DateTime.Compare(DateTime.Today, km.NGAYKT.Value) <= 0
                                       select ct.PHANTRAMKM.Value).ToList();
                if (listValue != null && listValue.Count >= 1)
                {
                    result = listValue.Max();
                }
            }
            return result;
        }
    }
}